using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ruiz_Jonathan_Examen.Data;
using Ruiz_Jonathan_Examen.Models;

namespace Ruiz_Jonathan_Examen.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class Ruiz_Jonathan_ClienteController : ControllerBase {
    private readonly Ruiz_Jonathan_DBContext _context;

    public Ruiz_Jonathan_ClienteController(Ruiz_Jonathan_DBContext context) {
      _context = context;
    }
    
    // PIMER LITERAL
    [HttpGet("/cantidadFacturas/{cedula}")]
    public async Task<ActionResult<int>> GetClientesFacturas(string cedula) {
      if (_context.Clientes == null) {
        return NotFound();
      }
      
      var cliente = await _context.Clientes.Where(c => c.Cedula.Equals(cedula)).FirstAsync();
      var cantidad = await _context.Facturas.Where(f => f.Cliente == cliente.Id).CountAsync();
      // var cantidadFacturas = cliente.Facturas.Count;

      return cantidad;
    }

    private bool ClienteExists(int id) {
      return (_context.Clientes?.Any(e => e.Id == id)).GetValueOrDefault();
    }
  }
}