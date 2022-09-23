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
  public class Ruiz_Jonathan_FacturasController : ControllerBase {
    private readonly Ruiz_Jonathan_DBContext _context;

    public Ruiz_Jonathan_FacturasController(Ruiz_Jonathan_DBContext context) {
      _context = context;
    }
    
    // LITERAL 2
    // POST: api/Ruiz_Jonathan_Facturas
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Factura>> PostFactura(Factura factura) {
      if (_context.Facturas == null) {
        return Problem("Entity set 'Ruiz_Jonathan_DBContext.Facturas'  is null.");
      }

      _context.Facturas.Add(factura);
      try {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateException) {
        if (FacturaExists(factura.Id)) {
          return Conflict();
        }
        else {
          throw;
        }
      }

      return CreatedAtAction("GetFactura", new { id = factura.Id }, factura);
    }
    
    // LITERAL 3
    [HttpGet("cantidadArticulos/{idFactura}")]
    public async Task<ActionResult<int>> GetFacturasArticulos(int idFactura) {
      if (_context.Facturas == null) {
        return NotFound();
      }
      
      var detalleFactura = await _context.DetalleFacturas.Where(d => d.Factura == idFactura).FirstAsync();
      
      var cantidadArticulos = detalleFactura.Cantidad;

      return Ok(new { cantidadArticulos });
    }


    // GET: api/Ruiz_Jonathan_Facturas/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Factura>> GetFactura(int id) {
      if (_context.Facturas == null) {
        return NotFound();
      }

      var factura = await _context.Facturas.FindAsync(id);

      if (factura == null) {
        return NotFound();
      }

      return factura;
    }
    
    private bool FacturaExists(int id) {
      return (_context.Facturas?.Any(e => e.Id == id)).GetValueOrDefault();
    }
  }
}