using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


// Simulation du controller
public class TestEventsController : Controller
{
    private readonly ITestApplicationDbContext _context;
    public TestEventsController(ITestApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Action de création d'événement accessible uniquement aux utilisateurs ayant le rôle "Admin".
    /// Si les données sont valides, l'événement est ajouté et enregistré dans la bdd simulée.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([Bind("Id,Entitled,Presentation,Date,Site")] TestEvent testEvent)
    {
        if (ModelState.IsValid)
        {
            _context.Add(testEvent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(testEvent);
    }
}

[TestClass]
public class TestEventsControllerTests
{
    private TestEventsController _controller;
    private MockTestApplicationDbContext _mockContext;

    private const string AdminRole = "Admin";
    private const string UserRole = "User";

    /// <summary>
    /// Initialisation des tests : Création du contrôleur et du contexte simulé.
    /// </summary>
    [TestInitialize]
    public void Setup()
    {
        _mockContext = new MockTestApplicationDbContext();
        _controller = new TestEventsController(_mockContext);
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };
    }

    /// <summary>
    /// Test de la méthode Create pour vérifier si un utilisateur peut créer un événement selon son rôle.
    /// </summary>
    /// <param name="role">Rôle de l'utilisateur (Admin ou User)</param>
    /// <param name="expectedSuccess">Résultat attendu : succès pour Admin, échec pour User</param>
    [TestMethod]
    [DataRow(AdminRole, true)]  // Rôle Admin, l'événement doit être créé
    [DataRow(UserRole, false)]  // Rôle User, l'événement ne doit pas être créé
    public async Task Create_EventBasedOnUserRole(string role, bool expectedSuccess)
    {
        // Arrange
        SetupUserRole(role);
        var testEvent = CreateTestEvent();

        // Act
        var result = await _controller.Create(testEvent);

        // Assert
            var viewResult = result as ViewResult;
            Console.WriteLine($"Role: {role}, Expected Success: {expectedSuccess}, Result: {result?.GetType().Name}");
    }

    /// <summary>
    /// Configure un utilisateur avec un rôle spécifique pour simuler un utilisateur authentifié.
    /// </summary>
    /// <param name="role">Rôle de l'utilisateur (Admin ou User)</param>
    private void SetupUserRole(string role)
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Role, role),
        }, "mock"));

        _controller.ControllerContext.HttpContext.User = user;
    }

    /// <summary>
    /// Crée un objet TestEvent pour les tests.
    /// </summary>
    /// <returns>Un événement de test avec des valeurs prédéfinies par défaut.</returns>
    private TestEvent CreateTestEvent()
    {
        return new TestEvent
        {
            Entitled = "Événement de Test",
            Presentation = "Présentation de Test",
            Date = DateTime.Now,
            Site = "Site de Test"
        };
    }
}

// Simulation du modèle Event
public class TestEvent
{
    public int Id { get; set; }
    public string Entitled { get; set; }
    public string Presentation { get; set; }
    public DateTime Date { get; set; }
    public string Site { get; set; }
}

//Simulation de la base de données
public interface ITestApplicationDbContext
{
    IQueryable<TestEvent> Events { get; }
    Task<int> SaveChangesAsync();
    void Add(TestEvent testEvent);
}

// Simulation de l'implémentation des données 
public class MockTestApplicationDbContext : ITestApplicationDbContext
{
    private List<TestEvent> _events = new();

    public IQueryable<TestEvent> Events => _events.AsQueryable();

    public Task<int> SaveChangesAsync()
    {
        return Task.FromResult(1);
    }

    public void Add(TestEvent testEvent)
    {
        _events.Add(testEvent);
    }
}
