using Microsoft.AspNetCore.Mvc;
using TLCOnline.Interfaces;
using TLCOnline.Models;
using TLCOnline.Resources;

namespace TLCOnline.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult List()
        {
            var customers = _customerService.GetAllCustomers();
            return View(customers);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(CustomerModel model)
        {
            if (ModelState.IsValid)
            {
                var exists = _customerService.CheckIfCustomerExists(model.CustMyKad, model.Email);
                if (exists)
                {
                    ViewData["AlertMessage"] = Labels.CustMyKadOrEmailAlreadyExists;
                    ViewData["AlertTitle"] = Labels.AddNewCustFailed;
                    return View(model);
                }

                _customerService.AddCustomer(model);
                return RedirectToAction(nameof(List));
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Update(string id)
        {
            var customer = _customerService.GetCustomerById(id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult Update(CustomerModel model)
        {
            if (ModelState.IsValid)
            {
                var exists = _customerService.CheckIfEmailExists(model.Email, model.CustMyKad);
                if (exists)
                {
                    ViewData["AlertMessage"] = Labels.CustEmailAlreadyExists;
                    ViewData["AlertTitle"] = Labels.UpdateCustFailed;
                    return View(model);
                }

                _customerService.UpdateCustomer(model);
                return RedirectToAction(nameof(List));
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult CustomerView(string id)
        {
            var customer = _customerService.GetCustomerById(id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            _customerService.DeleteCustomer(id);
            return RedirectToAction(nameof(List));
        }
    }
}
