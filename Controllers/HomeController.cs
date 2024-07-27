using Dashboard.Data;
using Dashboard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;


namespace Dashboard.Controllers
{
    public class HomeController : Controller
    {
		private readonly ILogger<HomeController> _logger;
		private readonly ApplicationDbContext _context;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public HomeController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_webHostEnvironment = webHostEnvironment;
		}

		[Authorize]
        public IActionResult Index()
        {
            var username = HttpContext.User.Identity.Name ?? null;

            HttpContext.Session.SetString("userdata", username);

            ViewBag.Username = username;


            return View();
        }


        public IActionResult AddNewItems()
        {
            var products = _context.products.ToList();

            ViewBag.Products = products;

            ViewBag.Username = HttpContext.Session.GetString("userdata");

            
           return View(products);
        }
        public IActionResult CreateProducts(Products products)
        {
                _context.products.Add(products);
                _context.SaveChanges();

                return RedirectToAction("AddNewItems");
           
        }
		public JsonResult GetData(int id)
		{
			var product = _context.products.SingleOrDefault(p => p.Id == id);

			if (product != null)
			{
				return Json(product);
			}
			else
			{
				return Json(null);
			}
		}
		public IActionResult UpdateProducts(Products product)
        {
            if (ModelState.IsValid)
            {
                _context.products.Update(product);
                _context.SaveChanges();
                TempData["upd"] = true;

                return RedirectToAction("AddNewItems");
            }
            TempData["upd"] = false;

            return View("EditProducts", product);
        }
    

        public JsonResult DeleteItem(int record_no)
        {
            var productdel = _context.products.SingleOrDefault(p => p.Id == record_no);//serch on record 

            if (productdel != null)
            {
                _context.products.Remove(productdel);
                _context.SaveChanges();
                TempData["del"] = true;
            }

            else
            {
                TempData["del"] = false;
            }

            return Json("Ok");

        }

        public IActionResult Privacy()
        {
            return View();
        }


		[Route("createDetails")]
		//to add the product details (Create)
		public IActionResult CreateDetails(ProductsDetails productDetails, IFormFile photo)
		{

			if (photo == null || photo.Length == 0)
			{
				return Content("File not selected");
			}
			//to save the image in the server (local host)
			var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", photo.FileName);
			using (FileStream stream = new FileStream(path, FileMode.Create))
			{
				photo.CopyTo(stream);
				stream.Close();
			}
			productDetails.Images = photo.FileName;  //to save the name of the image in the database
			_context.productDetails.Add(productDetails);
			_context.SaveChanges();

			return RedirectToAction("ProductDetails");
		}

		//to show the product details (Read)
		public IActionResult ProductDetails()
		{

			var ProductDetails = _context.productDetails.Join( //join between the two tables
				_context.products,
				pdetails => pdetails.ProductId,
				p => p.Id,
				(pdetails, p) => new
				{
					id = pdetails.Id,
					name = p.Name,
					color = pdetails.Color,
					price = pdetails.Price,
					qty = pdetails.Qty,
					image = pdetails.Images
				}
			).ToList(); //to show the data in the view

			var products = _context.products.ToList();

			ViewBag.Products = products;
			ViewBag.ProductDetails = ProductDetails;

			return View();
		}
		//to remove the product details (Delete)
		public JsonResult DeleteDetails(int record_no)
		{
			//to search for the record
			var productDdel = _context.productDetails.SingleOrDefault(p => p.Id == record_no);

			if (productDdel != null)
			{
				_context.productDetails.Remove(productDdel); //to remove the record
				_context.SaveChanges();
				TempData["del"] = true;
			}
			else
			{
				TempData["del"] = false;
			}

			return Json("Ok");
		}

		//to edit the product details (Update)
		public IActionResult UpdateDetails(ProductsDetails productDetails, IFormFile photo)
		{
			// Validate the fields
			if (productDetails.Price > 0  && productDetails.Color != null && productDetails.Color.Length > 0)
			{
				if (photo != null) // If a new photo is uploaded
				{
					// Save the image to the server (local host)
					var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", photo.FileName);
					using (FileStream stream = new FileStream(path, FileMode.Create))
					{
						photo.CopyTo(stream);
						stream.Close();
					}
					productDetails.Images = photo.FileName;  // Save the name of the image in the database
				}
				else
				{
					// If no new photo is uploaded, keep the existing photo
					var existingProduct = _context.productDetails.SingleOrDefault(p => p.Id == productDetails.Id);

					if (existingProduct != null)
					{
						productDetails.Images = existingProduct.Images;
					}
				}

				// Update the product details in the database
				var originalProduct = _context.productDetails.SingleOrDefault(p => p.Id == productDetails.Id);

				if (originalProduct != null)
				{
					originalProduct.Color = productDetails.Color;
					originalProduct.Price = productDetails.Price;
					originalProduct.Qty = productDetails.Qty;
					originalProduct.Images = productDetails.Images;

					_context.SaveChanges();
				}

				return RedirectToAction("ProductDetails");
			}

			return View(productDetails);
		}

		public JsonResult GetDetails(int id)
		{
			var productDetails = _context.productDetails.SingleOrDefault(p => p.Id == id); //to search for the record
			if (productDetails != null)
			{
				Console.WriteLine($"Record found: ID = {productDetails.Id}");

				return Json(productDetails);
			}
			else
			{
				Console.WriteLine("No record found for ID: " + id);

				return Json(null);
			}
		}
        public IActionResult CreateDamaged(DamagedProducts DamagedProducts)
        {

            if (ModelState.IsValid)
            {
                _context.damagedProducts.Add(DamagedProducts);
                _context.SaveChanges();
            }

            return RedirectToAction("DamagedProducts");
        }
		//to show the damaged products
		public IActionResult DamagedProducts()
		{
			var DamagedProducts = _context.damagedProducts.Join(
			 _context.productDetails,
			 pdamage => pdamage.ProductId,  // this is for the damaged products
			 pdetails => pdetails.ProductId, // this is for the product details
			 (pdamage, pdetails) => new
			 {
				 pdamage,
				 pdetails
			 } // combine the two tables
		 ).Join
		 ( // join the result with the products table
			 _context.products,
			 combined => combined.pdetails.ProductId,  // Key from the first join result (ProductDetails.ProductId)
			 p => p.Id, // this is for the products
			 (combined, p) => new
			 {
				 id = combined.pdamage.Id,
				 name = p.Name,
				 qty = combined.pdamage.Qty,
				 color = combined.pdetails.Color
			 }
		 ).ToList();

			var products = _context.products.ToList();

			ViewBag.Products = products;
			ViewBag.DamagedProducts = DamagedProducts;

			return View();
		}
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
