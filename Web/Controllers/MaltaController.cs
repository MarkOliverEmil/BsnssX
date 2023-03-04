using BsnssX.Core.Models;
using BsnssX.Core.Tax.Malta;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Web.Controllers
{
    public class MaltaController : Controller
    {
        static TaxResult Calc(int year, decimal income)
        {
            var tax = TaxCalculatorMalta.CalculateTax(year, income);
            TaxResult res = new TaxResult()
            {
                Income = income,
                Tax = TaxCalculatorMalta.CalculateTax(year, income),
                SocialSecurity = TaxCalculatorMalta.CalcSocialSecurityContributionsSum(year, income)
            };
            return res;
        }
        void Fill(MaltaTax mt)
        {
            var y = mt.Year = DateTime.Now.Year;
            mt.SocialSecurityContributions = new List<SocialSecurityResult>();
            mt.SocialSecurityContributions.Add(TaxCalculatorMalta.CalcSocialSecurityContributions(mt.Year));
            mt.SocialSecurityContributions.Add(TaxCalculatorMalta.CalcSocialSecurityContributions(mt.Year - 1));
            mt.SocialSecurityContributions.Add(TaxCalculatorMalta.CalcSocialSecurityContributions(mt.Year - 2));

            mt.TaxResults = new List<TaxResult>();

            mt.TaxResults.Add(Calc(y, 10000));
            mt.TaxResults.Add(Calc(y, 16000));
            mt.TaxResults.Add(Calc(y, 20000));
            mt.TaxResults.Add(Calc(y, 30000));
            mt.TaxResults.Add(Calc(y, 40000));
            mt.TaxResults.Add(Calc(y, 50000));
            mt.TaxResults.Add(Calc(y, 60000));
            mt.TaxResults.Add(Calc(y, 80000));
            mt.TaxResults.Add(Calc(y, 100000));
            mt.TaxResults.Add(Calc(y, 500000));
            mt.TaxResults.Add(Calc(y, 1000000));                        
        }
        void Calc(MaltaTax mt)
        {
            mt.Tax = (int)TaxCalculatorMalta.CalculateTax(mt.Year, mt.Income);
            mt.SocialSecurity = (int)TaxCalculatorMalta.CalcSocialSecurityContributionsSum(mt.Year, mt.Income);
        }

        public ActionResult Index()
        {
            MaltaTax mt = new MaltaTax();
            mt.Income = new Random().Next(10, 100) * 1000;
            Fill(mt);
            Calc(mt);
            return View(mt);
        }
        [HttpPost]
        public ActionResult Index(MaltaTax model)
        {
            model.Year = DateTime.Now.Year;            
            var inc = model.Income;
            Fill(model);
            Calc(model);            
            model.Income = inc;
            return View(model);
        }     
    }
}
