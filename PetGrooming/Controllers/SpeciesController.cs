using System;
using System.Collections.Generic;
using System.Data;
//required for SqlParameter class
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PetGrooming.Data;
using PetGrooming.Models;
using System.Diagnostics;

namespace PetGrooming.Controllers
{
    public class SpeciesController : Controller
    {
        private PetGroomingContext db = new PetGroomingContext();
        // GET: Species
        public ActionResult Index()
        {
            return View();
        }

        //TODO: Each line should be a separate method in this class
        // List
        public ActionResult List()
        {
            //what data do we need?
            List<Species> myspecies = db.Species.SqlQuery("Select * from species").ToList();

            return View(myspecies);
        }

        //Create the method for Add function - needs to return to the View folder
        public ActionResult Add()
        {
            return View();
        }

        //create a processing function for when user submits the Add form
        [HttpPost]
        public ActionResult Add(string SpeciesName)
        {
            //write the SQL query to insert into the table
            string query = "insert into species (Name) values (@SpeciesName)";
            //create the parameter we are using in the SQL query
            SqlParameter param = new SqlParameter("@SpeciesName", SpeciesName);
            //execute sql query
            db.Database.ExecuteSqlCommand(query, param);
            //on submit, determine where you want users to lead to - this is where your return will point to
            // user experience: either go to the thing you added, or the list of things you added to
            return RedirectToAction("List");
        }

        //create Update method for Update View to work with

        public ActionResult Update(int id)
        {
            //we need the species name, get that with an SQL query
            string query = "select * from species where SpeciesID = @id";
            //create your parameter to pass through
            SqlParameter param = new SqlParameter("@id", id);

            //we need to get a specific record - not just getting all species. Create a specific query to select only one record
            Species selectedspecies = db.Species.SqlQuery(query, param).FirstOrDefault();
            // pass that record that we have selected into the view, so we are only seeing one result
            return View(selectedspecies);
        }

        // Now we need to create the result for what happens when it is sent to the server 
        [HttpPost]
        public ActionResult Update(int id, string SpeciesName)
        {
            //Step One: Write Query
            string query = "update species set Name=@SpeciesName where SpeciesID=@id";
            //Step Two:Parameters
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@SpeciesName", SpeciesName);
            sqlparams[1] = new SqlParameter("@id", id);
            //Step Three: Execute Query
            db.Database.ExecuteSqlCommand(query, sqlparams);
            //Step Four: Redirect
            return RedirectToAction("List");
        }

        // Create a Show function

        public ActionResult Show(int id)
        {
            string query = "select * from species where SpeciesID = @id";
            SqlParameter param = new SqlParameter("@id", id);

            Species selectedspecies = db.Species.SqlQuery(query, param).First();

            return View(selectedspecies);
        }


        //create delete method

        public ActionResult Delete(int id)
        {
            string query = "delete from species where SpeciesID=@id";
            SqlParameter sqlparam = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, sqlparam);

            return RedirectToAction("List");
        }

    }
}