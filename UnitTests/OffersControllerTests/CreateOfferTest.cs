using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

#region Modèles et Simulation du contexte

/// <summary>
/// Modèle représentant un événement (Event).
/// </summary>
public class Event
{
    public int Id { get; set; }            // Identifiant de l'événement.
    public string Entitled { get; set; }    // Titre de l'événement.
    public DateTime Date { get; set; }      // Date de l'événement.
    public string Site { get; set; }        // Lieu où se déroule l'événement.
}

/// <summary>
/// Modèle représentant une offre associée à un événement.
/// </summary>
public class Offer
{
    public int OfferId { get; set; }  
    public string Title { get; set; } 
    public string Description { get; set; } 
    public string Place { get; set; }     
    public decimal Price { get; set; }    
    public int EventId { get; set; }     
}

/// <summary>
/// Interface représentant la base de données fictive
/// </summary>
public interface IApplicationDbContext
{
    IQueryable<Event> Events { get; }       // Liste des événements dans la base de données.
    IQueryable<Offer> Offers { get; }       // Liste des offres dans la base de données.
    Task<int> SaveChangesAsync();           // Méthode pour sauvegarder les modifications dans la base de données.
    void Add(Offer offer);                  // Méthode pour ajouter une nouvelle offre dans la base de données.
}

/// <summary>
/// Classe simulant le contexte de base de données pour les tests unitaires (évènements et offres)
/// </summary>
public class MockApplicationDbContext : IApplicationDbContext
{
    private List<Event> _events = new();
    private List<Offer> _offers = new(); 

    public IQueryable<Event> Events => _events.AsQueryable();  
    public IQueryable<Offer> Offers => _offers.AsQueryable();

    /// <summary>
    /// Simule la sauvegarde des modifications
    /// </summary>
    public Task<int> SaveChangesAsync()
    {
        return Task.FromResult(1);
    }

    /// <summary>
    /// Ajoute une nouvelle offre à la liste simulée des offres.
    /// </summary>
    public void Add(Offer offer)
    {
        _offers.Add(offer);
    }
}

#endregion

#region Contrôleur et Tests unitaires

/// <summary>
/// Contrôleur simulé pour gérer les actions liées aux offres.
/// </summary>
public class OffersController : Controller
{
    private readonly IApplicationDbContext _context;

    /// <summary>
    /// Constructeur du contrôleur, prenant un contexte de base de données comme paramètre.
    /// </summary>
    public OffersController(IApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Action pour créer une nouvelle offre. Seuls les utilisateurs avec le rôle "Admin" peuvent effectuer cette action.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Offer offer)
    {
        if (User.IsInRole("Admin"))
        {
            _context.Add(offer);                 
            await _context.SaveChangesAsync();   
            return RedirectToAction("Index");  
        }
        return View(offer);
    }
    public IActionResult Index() => View();
}

/// <summary>
/// Classe de test unitaire pour le contrôleur OffersController.
/// </summary>
[TestClass]
public class OffersControllerTests
{
    private OffersController? _controller;      
    private MockApplicationDbContext? _mockContext;

    /// <summary>
    /// Méthode de configuration exécutée avant chaque test. Initialisation du contexte simulé et le contrôleur.
    /// </summary>
    [TestInitialize]
    public void Setup()
    {
        _mockContext = new MockApplicationDbContext();    
        _controller = new OffersController(_mockContext);   
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()        
        };
    }

    /// <summary>
    /// Test pour vérifier que la création d'une offre réussit lorsque l'utilisateur est un administrateur.
    /// </summary>
    [TestMethod]
    public async Task Create_AdminUser_SuccessfulCreation()
    {
        // Arrange: Configure le rôle de l'utilisateur en tant qu'administrateur puis on créé une fausse offre
        SetupUserRole("Admin");    
        var offer = new Offer 
        {
            Title = "Test Offer",
            Description = "Test Description",
            Place = "Test Place",
            Price = 100,
            EventId = 1
        };

        // Act
        var result = await _controller.Create(offer) as RedirectToActionResult;  
        // Assert
        Assert.IsNotNull(result);                          
        Assert.AreEqual("Index", result.ActionName);     
        Assert.AreEqual(1, _mockContext.Offers.Count());  
        Console.WriteLine($"Offer créée avec succès: {_mockContext.Offers.First().Title}");
    }

    /// <summary>
    /// Test pour vérifier que les utilisateurs non-admin ne peuvent pas créer une offre.
    /// </summary>
    [TestMethod]
    public async Task Create_NonAdminUser_Unauthorized()
    {
        // Arrange: on créé une offre avec un utilisateur standard
        SetupUserRole("User");    
        var offer = new Offer     
        {
            Title = "Test Offer",
            Description = "Test Description",
            Place = "Test Place",
            Price = 100,
            EventId = 1
        };

        // Act
        var result = await _controller.Create(offer) as ViewResult;

        // Assert: On vérifie que la vue est retournée au lieu d'une redirection et que l'offre n'a pas été ajoutée dans la bdd.
        Assert.IsNotNull(result);                            
        Assert.AreEqual(0, _mockContext.Offers.Count());    
        Console.WriteLine("Unauthorized access: Seul un admin peut créer une offre");
    }

    /// <summary>
    /// Configure le rôle de l'utilisateur dans le contexte HTTP simulé pour les tests.
    /// </summary>
    /// <param name="role">Rôle à attribuer à l'utilisateur (Admin ou User).</param>
    private void SetupUserRole(string role)
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Role, role)    // Assigne le rôle spécifié à l'utilisateur.
        }, "mock"));

        _controller.ControllerContext.HttpContext.User = user;   // Associe l'utilisateur au contexte HTTP simulé.
    }
}

#endregion
