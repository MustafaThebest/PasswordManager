using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Data;
using PasswordManager.Models;
using PasswordManager.Utilities;

namespace PasswordManager.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PasswordsController : ControllerBase
    {
        private readonly ApiContext _context;

        public PasswordsController(ApiContext context)
        {
            _context = context;
        }

        //Create/Edit
        [HttpPost]
        public JsonResult Add(Password password)
        {
            if (password.ID == 0)
            {
                if (password.EncryptedPassword != null)
                    password.EncryptedPassword = Base64Converter.Encode(password.EncryptedPassword);

                _context.Passwords.Add(password);
            }
            else
            {
                return new JsonResult(BadRequest());
            }

            _context.SaveChanges();

            return new JsonResult(Ok(password));
        }

        [HttpGet]
        public JsonResult GetList()
        {
            var result = _context.Passwords.ToList();

            return new JsonResult(Ok(result));
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            var result = _context.Passwords.Find(id);
            if (result == null)
            {
                return new JsonResult(NotFound());
            }
            return new JsonResult(Ok(result));
        }

        [HttpGet]
        public JsonResult GetWithDecryptedPassword(int id)
        {
            var result = _context.Passwords.Find(id);
            if (result == null)
            {
                return new JsonResult(NotFound());
            }

            if(result.EncryptedPassword != null)
                result.EncryptedPassword = Base64Converter.Decode(result.EncryptedPassword);

            return new JsonResult(Ok(result));
        }

        [HttpPut]
        public JsonResult Update(Password password)
        {
            var passwordInDb = _context.Passwords.Find(password.ID);

            if (passwordInDb == null)
            {
                return new JsonResult(NotFound());
            }

            passwordInDb.Category = password.Category;
            passwordInDb.App = password.App;
            passwordInDb.UserName = password.UserName;
            passwordInDb.EncryptedPassword = password.EncryptedPassword;

            _context.SaveChanges();

            return new JsonResult(Ok(passwordInDb));
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = _context.Passwords.Find(id);
            if (result == null)
            {
                return new JsonResult(NotFound());
            }

            _context.Remove(result);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }
    }
}