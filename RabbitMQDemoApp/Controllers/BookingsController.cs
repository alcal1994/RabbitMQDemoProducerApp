using Microsoft.AspNetCore.Mvc;
using RabbitMQDemoApp.Services;
using RabbitMQDemoApp.Controllers;
using RabbitMQDemoApp.Models;

namespace RabbitMQDemoApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingsController : ControllerBase
    {

        private readonly ILogger<BookingsController> _logger;
        private readonly IMessageProducer _messageProducer;

        //in-memory db
        public static readonly List<Booking> _bookings = new();

        public BookingsController(ILogger<BookingsController> logger, IMessageProducer messageProducer)
        {
            _logger = logger;
            _messageProducer = messageProducer;
        }

        [HttpPost]
        public IActionResult CreateBooking(Booking newBooking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _bookings.Add(newBooking);
            _messageProducer.SendingMessage<Booking>(newBooking);

            return Ok();
        }

    }
}
