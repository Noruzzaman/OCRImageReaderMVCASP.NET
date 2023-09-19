using Microsoft.AspNetCore.Mvc;
using DemoOCRNew.Models;
using System.Diagnostics;
using Tesseract;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.IO;


namespace DemoOCRNew.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserModel model, IFormFile formFile)
        {
            

            try
            {

                var selectedValuess = model.SelectLanguageType;

                var text = "";
                if (formFile != null)
                {
                    string fileName = formFile.FileName;

                    fileName = Path.GetFileName(fileName);

                    string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

                    var stream = new FileStream(uploadpath, FileMode.Create);

                    formFile.CopyToAsync(stream);

                    var path = @"D:\CODE\DemoOCRNew\DemoOCRNew\wwwroot\tessdata";//Your tessdata file location
                    string strresult = selectedValuess.ToString();
                   

                    if (strresult == LanguageType.English.ToString())
                    {
                        strresult = "eng";
                    }
                    else if (strresult == LanguageType.Bangla.ToString())
                    {
                        strresult = "ben";
                    }
                    else if (strresult == LanguageType.Danish.ToString())
                    {
                        strresult = "dan";
                    }
                   

                    var sourceFilePath = uploadpath;
                    using (var engine = new TesseractEngine(path, strresult))
                    {
                        engine.SetVariable("user_defined_dpi", "70"); //set dpi for supressing warning
                        using (var img = Pix.LoadFromFile(sourceFilePath))
                        {
                            using (var page = engine.Process(img))
                            {
                                text = page.GetText();
                                ViewBag.Header = text;
                            }
                        }
                    }

                    ViewBag.Message = text;
                }

            }

            catch

            {

                ViewBag.Message = "Error while uploading the files.";

            }

            return View();

        }
    }
}
