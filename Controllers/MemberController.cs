using HomeWork.Models;
using HomeWork.Models.ViewModels;
using HomeWork.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomeWork.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberRepository _memberRepository;
        public MemberController(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        [HttpGet]
        public IActionResult AddNewMember()
        {
            return View();
        }
        public async Task<IActionResult> GetAllMember(string searchName)
        {
            IEnumerable<MemberModel> members;
            //= await _memberRepository.GetAllMember();
            if (string.IsNullOrEmpty(searchName))
            {
                members = await _memberRepository.GetAllMember();
            }
            else
            {
                members = await _memberRepository.SearchMembersByName(searchName);
            }
            return View(members);
        }
        [HttpPost]
        public IActionResult AddNewMember(MemberViewModel memberModel)
        {

              _memberRepository.AddMember(memberModel);
            return RedirectToAction("GetAllMember");
        }
        public IActionResult DeleteMember(int id)
        {
            _memberRepository.DeleteMember(id);
            return RedirectToAction("GetAllMember");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string memberName, string memberPassword)
        {
            // set so lan nhap toi da va thoi gian khoa 
            const int maxAttempts = 3;
            const int lockoutMinutes = 1;

            // Lấy số lần đăng nhập không thành công và thời gian khóa từ session
            var memberLoginAttempts = HttpContext.Session.GetInt32("LoginAttempts") ?? 0;
            var lockoutEndTime = HttpContext.Session.GetString("LockoutEndTime");

            if (lockoutEndTime != null && DateTime.Parse(lockoutEndTime) > DateTime.Now)
            {
                ModelState.AddModelError("", "Bạn đã nhập sai quá nhiều lần. Vui lòng thử lại sau 30 phút.");
                // Người dùng bị khóa tài khoản
                //ViewBag.LockoutEndTime = lockoutEndTime;
                return View();
            }

            if (ModelState.IsValid)
            {
                var member = _memberRepository.FindMember(memberName.Trim(), memberPassword.Trim());
                if(member != null)
                {
                    HttpContext.Session.SetString("MemberName", member.MemberName);
                    HttpContext.Session.Remove("LoginAttempts");
                    HttpContext.Session.Remove("LockoutEndTime");
                    return RedirectToAction("GetAllMember");
                }
                else
                {
                    memberLoginAttempts++;
                    HttpContext.Session.SetInt32("LoginAttempts", memberLoginAttempts);

                    if (memberLoginAttempts >= maxAttempts)
                    {
                        HttpContext.Session.SetString("LockoutEndTime", DateTime.Now.AddMinutes(lockoutMinutes).ToString());
                        ModelState.AddModelError("", "Bạn đã nhập sai quá 3 lần. Vui lòng thử lại sau 30 phút.");
                        //ViewBag.LockoutEndTime = DateTime.Now.AddMinutes(lockoutMinutes).ToString();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Sai tên truy cập hoặc mật khẩu.");
                    }


                }
            }
            //ViewBag.ErrorMessage = "Sai tên truy cập or mật khẩu";
            return View();
        }
        [HttpGet]
        public IActionResult Welcome()
        {
            string memberName = HttpContext.Session.GetString("MemberName");
            if (string.IsNullOrWhiteSpace(memberName))
            {
                return RedirectToAction("Login");
            }    
            else
            {
                ViewBag.MemberName = memberName;
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateMember(int id)
        {
            MemberModel member = await _memberRepository.GetMemberById(id);
            var data = new UpdateMemberModel()
            {
                MemberName = member.MemberName,
                YearOfBirth = member.YearOfBirth,
                Phone = member.Phone,
                Gender = member.Gender
            };
            //var members = await _memberRepository.GetAllMember();
            //ViewBag.Tutorials = new SelectList(tutorials, "Id", "Name");
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateMember(UpdateMemberModel modifiedData)
        {

            if (!ModelState.IsValid)
            {
                return View(modifiedData);
            }
            //MemberModel member = await _memberRepository.GetMemberById(modifiedData.Id);
            _memberRepository.UpdateMember(modifiedData);
            return RedirectToAction("GetAllMember");
        }


    }
}
