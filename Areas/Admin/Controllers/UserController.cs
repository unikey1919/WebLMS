using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassLibrary1.DAO;
using ClassLibrary1.EF;
using WebLMS.Common;

namespace WebLMS.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(string searchString, int page=1, int pagesize=1)
        {
            var dao = new UserDAO();
            var model = dao.listAllPaging(searchString,page,pagesize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var user = new UserDAO().ViewDetailByID(id);
            return View(user);
        }

        [HttpPost]
        //Truyền vào 1 user
        public ActionResult Create(TAIKHOAN user)
        {
            if(ModelState.IsValid)
            {
                var dao = new UserDAO();

                //mã hóa password
                var encryptedMd5Pas = Encryptor.MD5Hash(user.PassWord);
                user.PassWord = encryptedMd5Pas;

                long id = dao.Insert(user);
                if (id > 0)
                {
                    return RedirectToAction("Index","User");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm user thành công");
                }
                
            }
            return View("Index");

        }

        [HttpPost]
        public ActionResult Edit(TAIKHOAN user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();

                if (!string.IsNullOrEmpty(user.PassWord))
                {
                    ModelState.AddModelError("", "Vui lòng nhập password");
                    var encryptedMd5Pas = Encryptor.MD5Hash(user.PassWord);
                    
                    user.PassWord = encryptedMd5Pas;
                    
                }
                //mã hóa password


                var result = dao.Update(user);
                if (result)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }

            }
            return View("Index");

        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new UserDAO().Delete(id);
            return RedirectToAction("Index");
        }
    }
}