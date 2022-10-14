using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using Server.Model;
using Server.Servers;
using Server.Service;
using System.IO;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CipherTextController : ControllerBase
    {
        public IChipherService ChipherService { get; }
        public ITextService TextServer { get; }

        private IWritableOptions<RsaKeys> Secrets { get; }

        public CipherTextController(
            IChipherService _chipherServise,
            ITextService _textServise,
            IWritableOptions<RsaKeys> writableLocations)
        {
            ChipherService = _chipherServise;
            TextServer = _textServise;
            Secrets = writableLocations;
        }

        [HttpGet]
        public IActionResult Welcome() => Ok("Welсome");

        [HttpGet("GetFileText")]
        public IActionResult GetFileText(string fileName)
        {
            DataResponse response;
            var sessionKey = ChipherService.GenerateKey();

            var text = TextServer.GetText(fileName);
            if (text is null)
            {
                response = new() { ErrorMessage = "Cannot Find This FIle" };

                return Ok(response);
            }

            var openRsaKey = Secrets.Value.OpenKey;
            response = new()
            {
                EncryptedSessionKey = ChipherService.RsaEncrypt(sessionKey, openRsaKey),
                EncryptedFileText = ChipherService.AesEncrypt(text, sessionKey)
            };

            return Ok(response);
        }

        [HttpPost("GetOpenRsaKey")]
        public IActionResult GetOpenRsaKey([FromBody]string key)
        {
            Secrets.ChangeAppSettingValue(opt =>  opt.OpenKey = key);
            return Ok();
        }
    }
}
