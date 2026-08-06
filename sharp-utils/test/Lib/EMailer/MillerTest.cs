using Microsoft.Extensions.Logging.Abstractions;
using PChouse.Utils.EMail;
using Moq;
using Microsoft.Extensions.Logging;
using System.Net.Mail;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Net.Mime;

namespace PChouse.Utils.Test.EMailer;

[TestClass]
public sealed class MailerTest
{

    [TestMethod]
    public void TestSendMail()
    {
        var ds = Path.DirectorySeparatorChar;

        var path = new Regex(@$"{ds}bin{ds}Debug{ds}.+$", RegexOptions.IgnoreCase).Replace(
                        Assembly.GetAssembly(this.GetType())?.Location ?? "",
                        ""
                        );

        path = $"{path}{ds}Resources{ds}test-email.pdf";

        var servicesMock = new Mock<IServiceProvider>();

        servicesMock.Setup(s => s.GetService(typeof(ILogger<Mailer>)))
                    .Returns(NullLogger<Mailer>.Instance);


        var mailFactory = new Mailer.MailerFactory(servicesMock.Object);

        using var mailer = mailFactory.Instance(new MailerOptions
        {
            EnableSsl = false,
            FromAddress = new MailAddress("from-test@mailer.test", "From-test"),
            IsHtml = true,
            SenderAddress = new MailAddress("sender-test@mailer.test", "Sender-test"),
            SmtpPort = 1025,
            SmtpServer = "127.0.0.1"
        });

        mailer.EmailTo.Add(new MailAddress("to-address@test.pt"));
        mailer.EmailCc.Add(new MailAddress("cc-address@test.pt"));
        mailer.EmailBcc.Add(new MailAddress("bcc-address@test.pt"));

        mailer.EmailSubject = "Email test";
        mailer.EmailBody = $"<p>Test email <strong>{DateTime.Now}</strong><p>";

        mailer.AddAttachmentPath(path, "test-email.pdf", new ContentType("application/pdf")).GetAwaiter().GetResult();
        mailer.SendAsync().GetAwaiter().GetResult();

        Assert.IsNull(mailer.SendError);

        Assert.HasCount(1, mailer.MailMessage.CC);
        Assert.HasCount(1, mailer.MailMessage.Bcc);

        Assert.AreEqual("cc-address@test.pt", mailer.MailMessage.CC[0].Address);
        Assert.AreEqual("bcc-address@test.pt", mailer.MailMessage.Bcc[0].Address);
    }

    [TestMethod]
    public void TestSendError()
    {
        Assert.Throws<MailerException>(() =>
        {
            var servicesMock = new Mock<IServiceProvider>();

            servicesMock.Setup(s => s.GetService(typeof(ILogger<Mailer>)))
                        .Returns(NullLogger<Mailer>.Instance);

            var mailFactory = new Mailer.MailerFactory(servicesMock.Object);

            using var mailer = mailFactory.Instance(options =>
            {
                options.EnableSsl = false;
                options.FromAddress = new MailAddress("from-test@mailer.test", "From-test");
                options.IsHtml = true;
                options.SenderAddress = new MailAddress("sender-test@mailer.test", "Sender-test");
                options.SmtpPort = 1025;
                options.SmtpServer = "1.0.0.1";
            },
            smtpClient: smtpClient =>
            {
                smtpClient.Timeout = 1999;
            });

            mailer.EmailTo.Add(new MailAddress("to-address@test.pt"));
            mailer.EmailCc.Add(new MailAddress("cc-address@test.pt"));
            mailer.EmailBcc.Add(new MailAddress("bcc-address@test.pt"));

            mailer.EmailSubject = "Email test";
            mailer.EmailBody = $"<p>Test email <strong>{DateTime.Now}</strong><p>";

            mailer.SendAsync().GetAwaiter().GetResult();

            Assert.IsNotNull(mailer.SendError);
        });
    }

    [TestMethod]
    [DataRow(-1)]
    [DataRow(0)]
    [DataRow(65536)]
    public void TestWrongPort(int port)
    {
        Assert.Throws<MailerException>(() =>
        {
            var servicesMock = new Mock<IServiceProvider>();

            servicesMock.Setup(s => s.GetService(typeof(ILogger<Mailer>)))
                        .Returns(NullLogger<Mailer>.Instance);

            var mailFactory = new Mailer.MailerFactory(servicesMock.Object);

            using var mailer = mailFactory.Instance(options =>
            {
                options.EnableSsl = false;
                options.FromAddress = new MailAddress("from-test@mailer.test", "From-test");
                options.IsHtml = true;
                options.SenderAddress = new MailAddress("sender-test@mailer.test", "Sender-test");
                options.SmtpPort = port;
                options.SmtpServer = "1.0.0.1";
            });
        });
    }

    [TestMethod]
    public void TestNoSmtpServer()
    {
        var exception = Assert.Throws<MailerException>(() =>
        {
            var servicesMock = new Mock<IServiceProvider>();

            servicesMock.Setup(s => s.GetService(typeof(ILogger<Mailer>)))
                        .Returns(NullLogger<Mailer>.Instance);

            var mailFactory = new Mailer.MailerFactory(servicesMock.Object);

            using var mailer = mailFactory.Instance(options =>
            {
                options.EnableSsl = false;
                options.FromAddress = new MailAddress("from-test@mailer.test", "From-test");
                options.IsHtml = true;
                options.SenderAddress = new MailAddress("sender-test@mailer.test", "Sender-test");
                options.SmtpPort = 1025;
                options.SmtpServer = " ";
            },
            smtpClient: smtpClient =>
            {
                smtpClient.Timeout = 1999;
            });

            mailer.EmailTo.Add(new MailAddress("to-address@test.pt"));
            mailer.EmailCc.Add(new MailAddress("cc-address@test.pt"));
            mailer.EmailBcc.Add(new MailAddress("bcc-address@test.pt"));

            mailer.EmailSubject = "Email test";
            mailer.EmailBody = $"<p>Test email <strong>{DateTime.Now}</strong><p>";

            mailer.SendAsync().GetAwaiter().GetResult();

            Assert.IsNotNull(mailer.SendError);
        });

        Assert.AreEqual("SMTP server address is not configured in options", exception.Message);
    }

    [TestMethod]
    public void TestNoFromAddress()
    {
        var exception = Assert.Throws<MailerException>(() =>
        {
            var servicesMock = new Mock<IServiceProvider>();

            servicesMock.Setup(s => s.GetService(typeof(ILogger<Mailer>)))
                        .Returns(NullLogger<Mailer>.Instance);

            var mailFactory = new Mailer.MailerFactory(servicesMock.Object);

            using var mailer = mailFactory.Instance(options =>
            {
                options.EnableSsl = false;
                options.IsHtml = true;
                options.SenderAddress = new MailAddress("sender-test@mailer.test", "Sender-test");
                options.SmtpPort = 1025;
                options.SmtpServer = "127.0.0.1";
            },
            smtpClient: smtpClient =>
            {
                smtpClient.Timeout = 1999;
            });


            mailer.EmailTo.Add(new MailAddress("to-address@test.pt"));
            mailer.EmailCc.Add(new MailAddress("cc-address@test.pt"));
            mailer.EmailBcc.Add(new MailAddress("bcc-address@test.pt"));

            mailer.EmailSubject = "Email test";
            mailer.EmailBody = $"<p>Test email <strong>{DateTime.Now}</strong><p>";

            mailer.SendAsync().GetAwaiter().GetResult();

            Assert.IsNotNull(mailer.SendError);
        });

        Assert.AreEqual("SMTP from address not set", exception.Message);
    }

    [TestMethod]
    public void TestNoEmailBody()
    {
        var exception = Assert.Throws<MailerException>(() =>
        {
            var servicesMock = new Mock<IServiceProvider>();

            servicesMock.Setup(s => s.GetService(typeof(ILogger<Mailer>)))
                        .Returns(NullLogger<Mailer>.Instance);

            var mailFactory = new Mailer.MailerFactory(servicesMock.Object);

            using var mailer = mailFactory.Instance(options =>
            {
                options.EnableSsl = false;
                options.IsHtml = true;
                options.FromAddress = new MailAddress("from-test@mailer.test", "Sender-test");
                options.SenderAddress = new MailAddress("sender-test@mailer.test", "Sender-test");
                options.SmtpPort = 1025;
                options.SmtpServer = "127.0.0.1";
            },
            smtpClient: smtpClient =>
            {
                smtpClient.Timeout = 1999;
            });


            mailer.EmailTo.Add(new MailAddress("to-address@test.pt"));
            mailer.EmailCc.Add(new MailAddress("cc-address@test.pt"));
            mailer.EmailBcc.Add(new MailAddress("bcc-address@test.pt"));

            mailer.EmailSubject = "Email test";

            mailer.SendAsync().GetAwaiter().GetResult();

            Assert.IsNotNull(mailer.SendError);
        });

        Assert.AreEqual("Is not possible to send the email because does not have body", exception.Message);
    }

    [TestMethod]
    public void TestNoEmailSubject()
    {
        var exception = Assert.Throws<MailerException>(() =>
        {
            var servicesMock = new Mock<IServiceProvider>();

            servicesMock.Setup(s => s.GetService(typeof(ILogger<Mailer>)))
                        .Returns(NullLogger<Mailer>.Instance);

            var mailFactory = new Mailer.MailerFactory(servicesMock.Object);

            using var mailer = mailFactory.Instance(options =>
            {
                options.EnableSsl = false;
                options.IsHtml = true;
                options.FromAddress = new MailAddress("from-test@mailer.test", "Sender-test");
                options.SenderAddress = new MailAddress("sender-test@mailer.test", "Sender-test");
                options.SmtpPort = 1025;
                options.SmtpServer = "127.0.0.1";
            },
            smtpClient: smtpClient =>
            {
                smtpClient.Timeout = 1999;
            });


            mailer.EmailTo.Add(new MailAddress("to-address@test.pt"));
            mailer.EmailCc.Add(new MailAddress("cc-address@test.pt"));
            mailer.EmailBcc.Add(new MailAddress("bcc-address@test.pt"));

            mailer.EmailBody = "Email body";

            mailer.SendAsync().GetAwaiter().GetResult();

            Assert.IsNotNull(mailer.SendError);
        });

        Assert.AreEqual("Is not possible to send email because does not have subject", exception.Message);
    }

    [TestMethod]
    public void TestNoEmailToAddress()
    {
        var exception = Assert.Throws<MailerException>(() =>
        {
            var servicesMock = new Mock<IServiceProvider>();

            servicesMock.Setup(s => s.GetService(typeof(ILogger<Mailer>)))
                        .Returns(NullLogger<Mailer>.Instance);

            var mailFactory = new Mailer.MailerFactory(servicesMock.Object);

            using var mailer = mailFactory.Instance(options =>
            {
                options.EnableSsl = false;
                options.IsHtml = true;
                options.FromAddress = new MailAddress("from-test@mailer.test", "Sender-test");
                options.SenderAddress = new MailAddress("sender-test@mailer.test", "Sender-test");
                options.SmtpPort = 1025;
                options.SmtpServer = "127.0.0.1";
            },
            smtpClient: smtpClient =>
            {
                smtpClient.Timeout = 1999;
            });


            mailer.EmailCc.Add(new MailAddress("cc-address@test.pt"));
            mailer.EmailBcc.Add(new MailAddress("bcc-address@test.pt"));

            mailer.EmailSubject = "Email test";
            mailer.EmailBody = "Email body";

            mailer.SendAsync().GetAwaiter().GetResult();

            Assert.IsNotNull(mailer.SendError);
        });

        Assert.AreEqual("Email without 'TO' address", exception.Message);
    }

}

