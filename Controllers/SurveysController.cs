using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestCoreApp.Models;
using TestCoreApp.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using TestCoreApp.Models.ViewModels.Surveys;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;

namespace TestCoreApp.Controllers
{
    public class SurveysController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> userManager;

        public SurveysController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            this._db = context;
            this.userManager = userManager;
        }

        // GET: Surveys
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Survey> mySurveys = await _db.Surveys.Include("Questions").ToListAsync();
            List<SurveyViewModel> svms = new List<SurveyViewModel>();
            foreach (Survey s in mySurveys)
            {
                var vm = new SurveyViewModel
                {
                    Name = s.Name,
                    Id = s.Id
                };
                var user = await userManager.GetUserAsync(HttpContext.User);
                if (user != null)
                {
                    var answerCount = _db.UserSurveyAnswers.Where(a => a.Answer.Question.BaseSurvey.Id == s.Id && a.User.Id == user.Id).Count();
                    if (answerCount == null)
                        vm.UserStatus = "NotStarted";
                    else if (answerCount == 0)
                        vm.UserStatus = "NotStarted";
                    else if (answerCount < s.Questions.Count)
                        vm.UserStatus = "Started";
                    else if (answerCount >= s.Questions.Count)
                        vm.UserStatus = "Finished";
                }
                svms.Add(vm);
            }

            return View(svms);
        }

        // GET: Surveys/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            
            var survey = await _db.Surveys
                .FirstOrDefaultAsync(m => m.Id == id);
            if (survey == null)
            {
                return NotFound();
            }

            SurveyViewModel vm = new SurveyViewModel();
            vm.Id = survey.Id;
            vm.Name = survey.Name;
            vm.Description = survey.Description;
            vm.useQuestionDescriptions = survey.useQuestionDescriptions;
            vm.useAnswerDescriptions = survey.useAnswerDescriptions;
            vm.Questions = new List<SurveyQuestionViewModel>();

            var surveyQuestions = _db.SurveyQuestions.Where(c => c.BaseSurvey.Id == survey.Id).OrderBy(c => c.Sort).ToList();
            foreach (var q in surveyQuestions)
            {
                SurveyQuestionViewModel qvm = new SurveyQuestionViewModel();
                qvm.Id = q.Id;
                qvm.Sort = q.Sort;
                qvm.Name = q.Name;
                qvm.Description = q.Description;
                qvm.Answers = new List<SurveyAnswerViewModel>();
                var surveyAnswers = _db.SurveyAnswers.Where(a => a.Question.Id == q.Id).OrderBy(c => c.Sort).ToList();
                foreach (var a in surveyAnswers)
                {
                    System.Diagnostics.Debug.WriteLine("Found a survey answer: " + a.Name);
                    SurveyAnswerViewModel avm = new SurveyAnswerViewModel();
                    avm.Id = a.Id;
                    avm.Sort = a.Sort;
                    avm.Name = a.Name;
                    avm.Description = a.Description;
                    qvm.Answers.Add(avm);
                }
                vm.Questions.Add(qvm);
            }

            return View(vm);
        }

        // GET: Surveys/Create
        [Authorize(Roles = "Admin,Creator")]
        public IActionResult Create()
        {
            return View();
        }
        
        // POST: Surveys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Creator")]
        public async Task<IActionResult> Create([Bind("Id,Name")] Survey survey)
        {
            if (ModelState.IsValid)
            {
                _db.Add(survey);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(survey);
        }

        // GET: Surveys/Edit/5
        [Authorize(Roles = "Admin,Creator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var survey = await _db.Surveys.FindAsync(id);
            if (survey == null)
            {
                return NotFound();
            }


            SurveyViewModel vm = new SurveyViewModel();
            vm.Id = survey.Id;
            vm.Name = survey.Name;
            vm.Description = survey.Description;
            vm.useQuestionDescriptions = survey.useQuestionDescriptions;
            vm.useAnswerDescriptions = survey.useAnswerDescriptions;
            vm.Questions = new List<SurveyQuestionViewModel>();


            var surveyQuestions = _db.SurveyQuestions.Where(c => c.BaseSurvey.Id == survey.Id).OrderBy(c => c.Sort).ToList();
            foreach (var q in surveyQuestions)
            {
                SurveyQuestionViewModel qvm = new SurveyQuestionViewModel();
                qvm.Id = q.Id;
                qvm.Sort = q.Sort;
                qvm.Name = q.Name;
                qvm.Description = q.Description;
                qvm.Answers = new List<SurveyAnswerViewModel>();
                var surveyAnswers = _db.SurveyAnswers.Where(a => a.Question.Id == q.Id).OrderBy(c => c.Sort).ToList();
                foreach (var a in surveyAnswers)
                {
                    System.Diagnostics.Debug.WriteLine("Found a survey answer: " + a.Name);
                    SurveyAnswerViewModel avm = new SurveyAnswerViewModel();
                    avm.Id = a.Id;
                    avm.Sort = a.Sort;
                    avm.Name = a.Name;
                    avm.Description = a.Description;
                    qvm.Answers.Add(avm);
                }
                vm.Questions.Add(qvm);
            }

            return View(vm);
        }

        // POST: Surveys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Creator")]
        public async Task<IActionResult> Edit(int id,  Survey survey)
        {
            if (id != survey.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(survey);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SurveyExists(survey.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(survey);
        }

        // GET: Surveys/Delete/5
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var survey = await _db.Surveys
                .FirstOrDefaultAsync(m => m.Id == id);
            if (survey == null)
            {
                return NotFound();
            }

            return View(survey);
        }

        // POST: Surveys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var survey = await _db.Surveys.FindAsync(id);
            _db.Surveys.Remove(survey);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SurveyExists(int id)
        {
            return _db.Surveys.Any(e => e.Id == id);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Creator")]
        public IActionResult saveSurvey(SurveyViewModel survey)
        {
            
            System.Diagnostics.Debug.WriteLine("Calling Save Survey! survey questions: " + survey.Questions.Count);
                        
            return Json(new
            {
                success = true,
                responseUrl = "/Home/Index/"
            });
        }



        [HttpPost]
        [Authorize(Roles = "Admin,Creator")]
        public  IActionResult addQuestion(int survey_id, int? question_id)
        {
            var survey = _db.Surveys
                .Include(s => s.Questions)
                .Where(s => s.Id == survey_id).FirstOrDefault();

            SurveyQuestion q = new SurveyQuestion
            {
                BaseSurvey = survey,
                Name = ".",
                Description = "."
            };
            survey.Questions.Add(q);
            _db.SaveChanges();

            return Json(new
            {
                success = true,
                responseUrl = "/Surveys/Edit/" + survey_id
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Creator")]
        public IActionResult removeQuestion(int survey_id, int question_id)
        {
            var survey = _db.Surveys
                .Include(s => s.Questions)
                .Where(s => s.Id == survey_id).FirstOrDefault();


            var q = _db.SurveyQuestions.Find(question_id);
            var deleteAnswers = _db.SurveyAnswers.Where(a => a.Question == q).ToList();

            foreach (var a in deleteAnswers)
            {
                _db.SurveyAnswers.Remove(a);
            }
            _db.SurveyQuestions.Remove(q);
            _db.SaveChanges();

            return Json(new
            {
                success = true,
                responseUrl = "/Surveys/Edit/" + survey_id
            });
        }


        [HttpPost]
        [Authorize(Roles = "Admin,Creator")]
        public IActionResult addAnswer(int survey_id, int question_id, int? answer_id)
        {
            var survey = _db.Surveys.Find(survey_id);

            var question = _db.SurveyQuestions
                .Include(q => q.Answers)
                .Where(q => q.Id == question_id).FirstOrDefault();

            var lastSort = 0;
            if (answer_id != null)
            {
                var answer = _db.SurveyAnswers.Find(answer_id);
                lastSort = answer.Sort;
            }

            //If we hit the + button for an answer, then insert this new answer right before that one.
            if (lastSort > 0)
            {
                var surveyAnswers = _db.SurveyAnswers.Where(a => a.Question.Id == question_id).Where(a => a.Sort >= lastSort).OrderBy(c => c.Sort).ToList();
                foreach (var sa in surveyAnswers)
                {
                    sa.Sort += 1;
                }
            }
            else  //Otherwise add it at the end.
            {
                var surveyAnswers = _db.SurveyAnswers.Where(a => a.Question.Id == question_id).OrderBy(c => c.Sort).ToList();
                if (surveyAnswers.Count > 0)
                    lastSort = surveyAnswers.Last().Sort + 1;
                else
                    lastSort = 1;
            }
            
            SurveyAnswer a = new SurveyAnswer
            {
                Question = question,
                survey = survey,
                Name = ".",
                Description = ".",
                Sort = lastSort
            };
            question.Answers.Add(a);
            _db.SaveChanges();

            return Json(new
            {
                success = true,
                responseUrl = "/Surveys/Edit/" + survey_id
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Creator")]
        public IActionResult removeAnswer(int survey_id, int question_id, int answer_id)
        {                        
            var a = _db.SurveyAnswers.Find(answer_id);
            var removeSort = a.Sort;

            _db.SurveyAnswers.Remove(a);

            var surveyAnswers = _db.SurveyAnswers.Where(a => a.Question.Id == question_id).Where(a => a.Sort > removeSort).OrderBy(a => a.Sort).ToList();
            foreach (var sa in surveyAnswers)
            {
                sa.Sort -= 1;
            }

            _db.SaveChanges();

            return Json(new
            {
                success = true,
                responseUrl = "/Surveys/Edit/" + survey_id
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Creator")]
        public IActionResult moveAnswerUp(int survey_id, int question_id, int answer_id)
        {
            var thisAnswer = _db.SurveyAnswers.Find(answer_id);
            var moveSort = thisAnswer.Sort;
            var otherAnswer = _db.SurveyAnswers.Where(a => a.Question.Id == question_id).Where(a => a.Sort == (moveSort-1)).FirstOrDefault();
            otherAnswer.Sort += 1;
            thisAnswer.Sort -= 1;
            _db.SaveChanges();

            return Json(new
            {
                success = true,
                responseUrl = "/Surveys/Edit/" + survey_id
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Creator")]
        public IActionResult moveAnswerDown(int survey_id, int question_id, int answer_id)
        {
            var thisAnswer = _db.SurveyAnswers.Find(answer_id);
            var moveSort = thisAnswer.Sort;
            var otherAnswer = _db.SurveyAnswers.Where(a => a.Question.Id == question_id).Where(a => a.Sort == (moveSort + 1)).FirstOrDefault();
            otherAnswer.Sort -= 1;
            thisAnswer.Sort += 1;
            _db.SaveChanges();


            return Json(new
            {
                success = true,
                responseUrl = "/Surveys/Edit/" + survey_id
            });
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> TakeSurvey(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var survey = await _db.Surveys.FindAsync(id);
            if (survey == null)
            {
                return NotFound();
            }
            
            SurveyViewModel svm = new SurveyViewModel();
            svm.Id = survey.Id;
            svm.Name = survey.Name;
            svm.Description = survey.Description;
            svm.useQuestionDescriptions = survey.useQuestionDescriptions;
            svm.useAnswerDescriptions = survey.useAnswerDescriptions;
            svm.Questions = new List<SurveyQuestionViewModel>();
            
            var surveyQuestions = _db.SurveyQuestions.Where(c => c.BaseSurvey.Id == survey.Id).OrderBy(c => c.Sort).ToList();
            foreach (var q in surveyQuestions)
            {
                SurveyQuestionViewModel qvm = new SurveyQuestionViewModel();
                qvm.Id = q.Id;
                qvm.Sort = q.Sort;
                qvm.Name = q.Name;
                qvm.Description = q.Description;
                qvm.Answers = new List<SurveyAnswerViewModel>();
                var surveyAnswers = _db.SurveyAnswers.Where(a => a.Question.Id == q.Id).OrderBy(c => c.Sort).ToList();
                foreach (var a in surveyAnswers)
                {
                    System.Diagnostics.Debug.WriteLine("Found a survey answer: " + a.Name);
                    SurveyAnswerViewModel avm = new SurveyAnswerViewModel();
                    avm.Id = a.Id;
                    avm.Sort = a.Sort;
                    avm.Name = a.Name;
                    avm.Description = a.Description;
                    qvm.Answers.Add(avm);
                }
                svm.Questions.Add(qvm);
            }
            
            return View(svm);
        }

        [HttpPost]
        [Authorize]       
        public async Task<IActionResult> TakeSurvey(string answers)
        {
            var ans = JsonConvert.DeserializeObject<int[]>(answers);
            foreach (var a in ans)
            {
                //System.Diagnostics.Debug.WriteLine("Answer: " + a);
                var answer = _db.SurveyAnswers.Where(m => m.Id == a).FirstOrDefault();
                var user = await userManager.GetUserAsync(HttpContext.User);
                UserSurveyAnswer ua = new UserSurveyAnswer
                {
                    Answer = answer,
                    User = user
                };
                _db.UserSurveyAnswers.Add(ua);
                user.PirateGold++;//Later: pay more for demographics questions, identify by having only one survey that is 
                //demographics, or else make it a survey type or category. (Better! That way demographics can be always evolving.)
            }
            _db.SaveChanges();


            return Json(new
            {
                success = true,
                responseUrl = "/Surveys/Index/"
            });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ReviewSurvey(int? id)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return RedirectToAction("Main", "Home");
            }

            if (id == null)
            {
                return NotFound();
            }

            var survey = await _db.Surveys.FindAsync(id);
            if (survey == null)
            {
                return NotFound();
            }

            SurveyViewModel svm = new SurveyViewModel();
            svm.Id = survey.Id;
            svm.Name = survey.Name;
            svm.Description = survey.Description;
            svm.useQuestionDescriptions = survey.useQuestionDescriptions;
            svm.useAnswerDescriptions = survey.useAnswerDescriptions;
            svm.Questions = new List<SurveyQuestionViewModel>();

            var surveyQuestions = _db.SurveyQuestions.Where(c => c.BaseSurvey.Id == survey.Id).OrderBy(c => c.Sort).ToList();
            foreach (var q in surveyQuestions)
            {
                SurveyQuestionViewModel qvm = new SurveyQuestionViewModel();
                qvm.Id = q.Id;
                qvm.Sort = q.Sort;
                qvm.Name = q.Name;
                qvm.Description = q.Description;
                qvm.Answers = new List<SurveyAnswerViewModel>();
                var surveyAnswers = _db.SurveyAnswers.Where(a => a.Question.Id == q.Id).OrderBy(c => c.Sort).ToList();
                var userAnswer = _db.UserSurveyAnswers.Where(a => a.Answer.Question.Id == q.Id && a.User.Id == user.Id).FirstOrDefault();
                foreach (var a in surveyAnswers)
                {
                    SurveyAnswerViewModel avm = new SurveyAnswerViewModel();
                    avm.Id = a.Id;
                    avm.Sort = a.Sort;
                    avm.Name = a.Name;
                    avm.Description = a.Description;
                    if (userAnswer.Answer.Id == a.Id)
                    {
                        avm.Checked = true;
                        System.Diagnostics.Debug.WriteLine("CHECKING a survey answer!!  " + a.Name);
                    }
                    qvm.Answers.Add(avm);
                    //HERE: we need a new property in the answer view model, to say if it is checked or not.
                }
                svm.Questions.Add(qvm);
            }

            return View(svm);
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateSurvey(string answers)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return RedirectToAction("Main", "Home");
            }
            var ans = JsonConvert.DeserializeObject<int[]>(answers);
            foreach (var a in ans)
            {
                System.Diagnostics.Debug.WriteLine("Update Answer: " + a);
                var answer = _db.SurveyAnswers.Where(m => m.Id == a).Include("Question").FirstOrDefault();
                var userAnswer = _db.UserSurveyAnswers.Where(a => a.Answer.Question.Id == answer.Question.Id && a.User.Id == user.Id).FirstOrDefault();
                if (userAnswer.Id != answer.Id)
                {
                    _db.UserSurveyAnswers.Remove(userAnswer);
                    UserSurveyAnswer ua = new UserSurveyAnswer
                    {
                        Answer = answer,
                        User = user
                    };
                    _db.UserSurveyAnswers.Add(ua);
                }
            }
            _db.SaveChanges();


            return Json(new
            {
                success = true,
                responseUrl = "/Surveys/Index/"
            });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ViewResults(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var survey = await _db.Surveys
                .FirstOrDefaultAsync(m => m.Id == id);
            if (survey == null)
            {
                return NotFound();
            }

            SurveyViewModel vm = new SurveyViewModel();
            vm.Id = survey.Id;
            vm.Name = survey.Name;
            vm.Description = survey.Description;
            vm.useQuestionDescriptions = survey.useQuestionDescriptions;
            vm.useAnswerDescriptions = survey.useAnswerDescriptions;
            vm.Questions = new List<SurveyQuestionViewModel>();

            var surveyQuestions = _db.SurveyQuestions.Where(c => c.BaseSurvey.Id == survey.Id).OrderBy(c => c.Sort).ToList();
            foreach (var q in surveyQuestions)
            {
                SurveyQuestionViewModel qvm = new SurveyQuestionViewModel();
                qvm.Id = q.Id;
                qvm.Sort = q.Sort;
                qvm.Name = q.Name;
                qvm.Description = q.Description;
                qvm.Answers = new List<SurveyAnswerViewModel>();
                var surveyAnswers = _db.SurveyAnswers.Where(a => a.Question.Id == q.Id).OrderBy(c => c.Sort).ToList();
                foreach (var a in surveyAnswers)
                {
                    System.Diagnostics.Debug.WriteLine("Found a survey answer: " + a.Name);
                    SurveyAnswerViewModel avm = new SurveyAnswerViewModel();
                    avm.Id = a.Id;
                    avm.Sort = a.Sort;
                    avm.Name = a.Name;
                    var voteCount = _db.UserSurveyAnswers.Where(m => m.Answer == a).Count();
                    avm.Description = voteCount.ToString();
                    qvm.Answers.Add(avm);
                }
                vm.Questions.Add(qvm);
            }

            return View(vm);
        }


        ////////////////////////////////////

        // GET: Surveys/Create
        [Authorize]
        public IActionResult AddFew()
        {
            /*
            var surveys = _db.Surveys.ToList();
            foreach (Survey s in surveys)
            {
                for (int c=1; c < 4; c++)
                {
                    SurveyQuestion q = new SurveyQuestion();
                    q.BaseSurvey = s;
                    q.Name = "Question_" + c.ToString();
                    q.Description = "Description_" + c.ToString();
                    _db.SurveyQuestions.Add(q);
                }                
            }   
            var questions = _db.SurveyQuestions.ToList();
            foreach (SurveyQuestion q in questions)
            {
                for (int c = 1; c < 4; c++)
                {
                    SurveyAnswer a = new SurveyAnswer();
                    a.Question = q;
                    a.Name = "Answer_" + c.ToString();
                    a.Description = "Description_" + c.ToString();
                    _db.SurveyAnswers.Add(a);
                }
            }
            _db.SaveChanges();                     
            */

            var answers = _db.SurveyQuestions.ToList();
            foreach (var a in answers)
            {
                System.Diagnostics.Debug.WriteLine("Answer name: " + a.Name);
            }

            return RedirectToAction("Main", "Home");
        }
    }
}
