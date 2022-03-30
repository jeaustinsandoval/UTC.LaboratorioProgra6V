using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WBL;

namespace WebApp.Pages.Proveedores
{
    public class GridModel : PageModel
    {
        private readonly IProveedorService proveedor;

        public GridModel(IProveedorService proveedor)
        {
            this.proveedor = proveedor;
        }

        public IEnumerable<ProveedorEntity> GridList { get; set; } = new List<ProveedorEntity>();

        public string Mensaje { get; set; } = "";

        public async Task<IActionResult> OnGet() 
        {

            try
            {
                GridList = await proveedor.GET();

                if (TempData.ContainsKey("Msg"))
                {
                    Mensaje = TempData["Msg"] as string;
                }

                TempData.Clear();

                return Page();
            }
            catch (Exception ex)
            {

                return Content(ex.Message);
            }
        
        }

        public async Task<IActionResult> OnGetEliminar(int id)   
        {

            try
            {
                var result = await proveedor.DELETE( new() 
                { IdProveedor=id});

                if (result.CodeError!=0)
                {
                    throw new Exception(result.MsgError);
                }

                TempData["Msg"] = "El registro fue eliminado satisfactoriamente";

                return Redirect("Grid");
            }
            catch (Exception ex)
            {

                return Content(ex.Message);
            }

        }
    }
}
