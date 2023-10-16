using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mvc.Models;

namespace mvc.Controllers;

public class AlunosController : Controller
{
    private readonly ILogger<AlunosController> _logger;

    public AlunosController(ILogger<AlunosController> logger)
    {
        _logger = logger;
    }

    [Route("/alunos")]
    public IActionResult Index()
    {
        // ViewBag.Aluno = new {
        //     Mensagem = "Testessssssss"
        // };

        // ViewData["Teste"] = "sssss";

        return View(Aluno.Todos());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
