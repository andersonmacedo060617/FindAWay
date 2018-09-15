using FindAWay.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindAWay.WebGame.Controllers
{
    public class FindWayController : Controller
    {
        // GET: FindWay

        public ActionResult Index(string difficulty = "Normal")
        {
            Game game;
            if (Session["Game"] == null)
            {
                game = new Game(Game.StrDifficultyToEnum(difficulty));
                Session.Add("Game", game);
            }
            else
            {
                game = (Game)Session["Game"];
                if (game.Difficulty != Game.StrDifficultyToEnum(difficulty))
                {
                    game = new Game(Game.StrDifficultyToEnum(difficulty));
                    Session.Remove("Game");
                    Session.Add("Game", game);
                }
                    
            }

            return View();
        }

        public PartialViewResult IndexGame()
        {
            Game game;
            if (Session["Game"] == null)
            {
                game = new Game(Game.StrDifficultyToEnum("Normal"));
                Session.Add("Game", game);
            }
            game = (Game)Session["Game"];

            if (game.GameOver())
                RedirectToAction("GameOver");

            if (game.GamerWin())
                RedirectToAction("GameWin");
                

            return PartialView(game);
        }

        public PartialViewResult GameOver()
        {
            return PartialView();
        }

        public PartialViewResult GameWin()
        {
            return PartialView();
        }



        public PartialViewResult LoadStep(int step)
        {
            Game game = (Game)Session["Game"];
            if(game.Difficulty == EDifficulty.Normal)
            {
                ViewBag.StyleStep = "col col-md-3";
            }else if(game.Difficulty == EDifficulty.Easy)
            {
                ViewBag.StyleStep = "col col-md-4";
            }else if (game.Difficulty == EDifficulty.Hard)
            {
                ViewBag.StyleStep = "col col-lg-2";
            }

            return PartialView(game.GameBoard.Fields.Where(x=>x.Step == step));
        }

        public PartialViewResult LoadField(int step, int position)
        {
            var game = (Game)Session["Game"];
            var field = game.GameBoard.Fields.Where(x => x.Step == step && x.Position == position).FirstOrDefault();


            if (!field.Visible)
            {
                ViewBag.ShowIcon = "?";
                ViewBag.Style = "alert alert-info";
                ViewBag.Action = game.StepLiberado(step) ? String.Format("selectPosition({0},{1})", step, position) : "";
            }
            else if(field.Visible && field.RightWay)
            {
                ViewBag.ShowIcon = "V";
                ViewBag.Style = "alert alert-success";
                ViewBag.Action = "";
            }
            else if(field.Visible && !field.RightWay)
            {
                ViewBag.ShowIcon = "X";
                ViewBag.Style = "alert alert-danger";
                ViewBag.Action = "";
            }
            
            return PartialView();
        }

        public ActionResult SelecionaPosicao(int step, int position)
        {
            Game game = (Game)Session["Game"];
            if(game.StepGamer == step)
            {
                game.SelectField(step, position);
            }

            return RedirectToAction("IndexGame", "FindWay");
        }
    }
}