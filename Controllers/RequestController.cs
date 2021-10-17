using CentWorkTimeTracker.Helpers;
using CentWorkTimeTracker.Models;
using CentWorkTimeTracker.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestController : Controller
    {
        private readonly IRequestRepository _requestsRepo;
        private readonly IEmailService _emailService;
        private readonly UserStatisticService _statService;
        private readonly IUserRepository _userRepository;

        public RequestController(IRequestRepository requestsRepo, IEmailService emailService, UserStatisticService statService, IUserRepository userRepository)
        {
            _requestsRepo = requestsRepo;
            _emailService = emailService;
            _statService = statService;
            _userRepository = userRepository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            if (!HttpContext.Session.GetInt32("userId").HasValue)
            {
                return Unauthorized();
            }
            int userId = HttpContext.Session.GetInt32("userId").Value;
            var requests = _requestsRepo.GetAllRequestsByUserId(userId);
            if (requests.Count == 0)
            {
                return NoContent();
            }
            return Ok(requests);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var request = await _requestsRepo.GetRequestById(id);
            return Ok(request);
        }

        [HttpGet]
        [Route("user/{email}")]
        public async Task<ActionResult> GetByUserEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                return NotFound(new { Message = $"User with ${email} not found." });
            }
            var request = _requestsRepo.GetAllRequestsByUserId(user.Id);
            return Ok(request);
        }


        [HttpGet]
        [Route("all")]
        public ActionResult GetAll()
        {
            var requests = _requestsRepo.GetAllInProgressRequests();
            return Ok(requests);
        }

        [HttpGet]
        [Route("all/rejected")]
        public ActionResult GetRejected()
        {
            var requests = _requestsRepo.GetAllRejectedRequests();
            return Ok(requests);
        }

        [HttpGet]
        [Route("all/approved")]
        public ActionResult GetApproved()
        {
            var requests = _requestsRepo.GetAllApprovedRequests();
            return Ok(requests);
        }

        [HttpPost]
        [Route("add/vacation")]
        public async Task<ActionResult> AddVacation([FromBody] AddVacationModel model)
        {
            int userId = HttpContext.Session.GetInt32("userId").Value;

            if (model.DateBegin.Date > model.DateEnd.Date)
            {
                return BadRequest(new { Message = "День начала не может быть позже дня окончания." });
            }

            int lastDays = 25 - _statService.GetDaysCountByUserid(userId, "Vacation", true);
            if (lastDays < (model.DateEnd - model.DateBegin).Days)
            {
                return BadRequest(new { Message = $"У вас нет столько Vacation. У вас осталось {lastDays} дней." });
            }
            Vacation vacation = new Vacation()
            {
                UserId = userId,
                DateBegin = model.DateBegin,
                DateEnd = model.DateEnd
            };
            var added = await _requestsRepo.AddRequest(vacation);
            _emailService.sendMessageToManager(added);
            return Ok(added);
        }

        [HttpPost]
        [Route("add/unpaidedvacation")]
        public async Task<ActionResult> AddUnpaidedVacation([FromBody] AddUnpaidedVacationModel model)
        {
            int userId = HttpContext.Session.GetInt32("userId").Value;

            if (model.DateBegin.Date > model.DateEnd.Date)
            {
                return BadRequest(new { Message = "День начала не может быть позже дня окончания." });
            }

            UnpaidedVacation unpaidedVacation = new UnpaidedVacation()
            {
                UserId = userId,
                DateBegin = model.DateBegin,
                DateEnd = model.DateEnd,
                Description = model.Description
            };
            var added = await _requestsRepo.AddRequest(unpaidedVacation);
            _emailService.sendMessageToManager(added);
            return Ok(added);
        }

        [HttpPost]
        [Route("add/sick")]
        public async Task<ActionResult> AddSick([FromBody] AddSickModel model)
        {
            int userId = HttpContext.Session.GetInt32("userId").Value;

            if (model.DateBegin.Date > model.DateEnd.Date)
            {
                return BadRequest(new { Message = "День начала не может быть позже дня окончания" });
            }

            Sick sick = new Sick()
            {
                UserId = userId,
                DateBegin = model.DateBegin,
                DateEnd = model.DateEnd,
                DocNumber = model.DocNumber
            };
            var added = await _requestsRepo.AddRequest(sick);
            _emailService.sendMessageToManager(added);
            return Ok(added);
        }

        [HttpPost]
        [Route("add/sickdays")]
        public async Task<ActionResult> AddSickDays([FromBody] AddSickDaysModel model)
        {
            int userId = HttpContext.Session.GetInt32("userId").Value;
            int lastDays = 5 - _statService.GetDaysCountByUserid(userId, "SickDays", true);
            if (lastDays < (model.DateEnd - model.DateBegin).Days)
            {
                return BadRequest(new { Message = $"У вас нет столько SickDays. У вас осталось {lastDays} дней." });
            }
            if (model.DateBegin.Date > model.DateEnd.Date)
            {
                return BadRequest(new { Message = "День начала не может быть позже дня окончания." });
            }
            SickDays sickDays = new SickDays()
            {
                UserId = userId,
                DateBegin = model.DateBegin,
                DateEnd = model.DateEnd,
                Description = model.Description
            };
            var added = await _requestsRepo.AddRequest(sickDays);
            _emailService.sendMessageToManager(added);
            return Ok(added);
        }

        [HttpPost]
        [Route("add/transfer")]
        public async Task<ActionResult> AddTransfer([FromBody] AddTransferModel model)
        {
            int userId = HttpContext.Session.GetInt32("userId").Value;
            if (model.DayFrom.Date < System.DateTime.Now.Date)
            {
                return BadRequest(new { Message = "Переносы задним числом запрещены." });
            }
            if (!DaysHelper.IsWorkDay(model.DayFrom) || DaysHelper.IsWorkDay(model.DayTo))
            {
                return BadRequest(new { Message = "Проверьте дни. Нельзя перенести выходной день, и нельзя перенести на рабочий день." });
            }
            if (model.DayFrom.Date > model.DayTo.Date)
            {
                return BadRequest(new { Message = "Нельзя перенести работу на день, который раньше переносимого." });
            }
            Transfer transfer = new Transfer()
            {
                UserId = userId,
                DayFrom = model.DayFrom.Date,
                DayTo = model.DayTo.Date,
                Description = model.Description
            };
            var added = await _requestsRepo.AddRequest(transfer);
            _emailService.sendMessageToManager(added);
            return Ok(added);
        }

        [HttpPost]
        [Route("add/wfh")]
        public async Task<ActionResult> AddWorkFromHome([FromBody] AddWorkFromHomeModel model)
        {
            if (!DaysHelper.IsWorkDay(model.Date))
            {
                return BadRequest(new { Message = "Работа из дома в хыходной день запрещена." });
            }
            if (model.Date.Date <= System.DateTime.Now.Date)
            {
                return BadRequest(new { Message = "Переносы задним числом запрещены." });
            }
            int userId = HttpContext.Session.GetInt32("userId").Value;
            WorkFromHome wfh = new WorkFromHome()
            {
                UserId = userId,
                Date = model.Date.Date
            };
            var added = await _requestsRepo.AddRequest(wfh);
            _emailService.sendMessageToManager(added);
            return Ok(added);
        }

        [HttpGet("approve/{id}")]
        //todo добавить проверку на тип пользователя
        public async Task<ActionResult> ApproveRequest(int id)
        {
            var request = await _requestsRepo.SetRequestStatus(id, RequestStatus.Approved);
            if (request == null)
            {
                return NotFound();
            }
            _emailService.sendMessageToUser(request);
            return Ok(request);
        }

        [HttpGet("reject/{id}")]
        //todo добавить проверку на тип пользователя
        public async Task<ActionResult> RejectRequest(int id)
        {
            var request = await _requestsRepo.SetRequestStatus(id, RequestStatus.Rejected);
            if (request == null)
            {
                return NotFound();
            }
            _emailService.sendMessageToUser(request);
            return Ok(request);
        }
    }
}