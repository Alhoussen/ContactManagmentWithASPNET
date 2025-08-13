using Microsoft.AspNetCore.Mvc;

namespace ContactManagement.Api.Controllers;

/// <summary>
/// Contrôleur principal pour les vues
/// </summary>
public class HomeController : Controller
{
    /// <summary>
    /// Page d'accueil avec la liste des contacts
    /// </summary>
    /// <returns>Vue principale</returns>
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// Page de détails d'un contact
    /// </summary>
    /// <param name="id">ID du contact</param>
    /// <returns>Vue de détails</returns>
    public IActionResult Details(int id)
    {
        ViewBag.ContactId = id;
        return View();
    }
}

