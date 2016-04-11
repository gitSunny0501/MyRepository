using Dapper;
using DapperDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DapperDemo.Controllers
{
    public class UserController : Controller
    {

        static readonly string connStr = "Data Source=10.1.25.8;Initial Catalog=TestDB;User Id=sa;Password=Lzhigang1990;";
        //
        // GET: /User/
        public ActionResult Index()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                string sql = "select AutoId,UserId,UserPwd from users with(nolock)";
                var userList = con.Query<User>(sql);
                return View(userList);
            }
        }

        //
        // GET: /User/Details/5

        public ActionResult Details(int id)
        {
            string sql = "select AutoId,UserId,UserPwd from users where AutoId=@AutoId";
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                var userList = con.Query<User>(sql, new { AutoId = id });
                ViewData.Model = userList;
                return View("Index");
            }
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(User user)
        {
            string sql = "insert into users values(@UserId,@UserPwd)";
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                int res = con.Execute(sql, user);
                if (res > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Create");
                }
            }
        }

        //
        // GET: /User/Edit/5

        public ActionResult Edit(int id)
        {
            string sql = "select AutoId,UserId,UserPwd from users with(nolock) where AutoId=@AutoId";
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                var user = con.Query<User>(sql, new { AutoId = id }).ToList().FirstOrDefault();
                return View(user);
            }
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(User user)
        {
            string sql = "update users set UserId=@UserId,UserPwd=@UserPwd where AutoId=@AutoId";
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                int res = con.Execute(sql, user);
                if (res > 0)
                {
                    return Redirect(Url.Action("Index"));
                }
                else
                {
                    return Redirect(Url.Action("Edit", new { id = user.AutoId }));
                }
            }
        }

        //
        // GET: /User/Delete/5
        public ActionResult Delete(int id)
        {
            string sql = "delete from users where AutoId=@AutoId";
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                int res = con.Execute(sql, new { AutoId = id });
                return RedirectToAction("Index");
            }
        }
    }
}
