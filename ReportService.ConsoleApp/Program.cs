using EmailSender;
using ReportService.Core.Domains;
using System.Collections.Generic;
using System;
using ReportService.Core;
using Cipher;

namespace ReportService.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            //var stringCipher = new StringCipher("1");
            //var encryptedPassword = stringCipher.Encrypt("hasło");
            //var decryptedPassword = stringCipher.Decrypt(encryptedPassword);


            //return;

            var emailReceiver = "rol23@poczta.fm";
            var htmlEmail = new GenerateHtmlEmail();

            var encryptedPassword = "nOwFp9/Zisoe44mILyb1ULuYhKLvQNliKmNuKzTk4ODgcipfj3kcxYixf8ksKQ+EE3cmypW6SWTq6RrB3ahYvgguBdz3EwKPKDS4pINjObb/7H+Ka+Y+R2SnLnHgAQjB";
            StringCipher stringCipher = new StringCipher("3D4E8871-46CF-4312-9F6C-F9FF2C897CE1");

            var email = new Email(new EmailParams
            {
                HostSmtp = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                SenderName = "ReportService Gmail",
                SenderEmail = "TestyAkademiaDotNet@gmail.com",
                SenderEmailPassword = stringCipher.Decrypt(encryptedPassword)
            });

            var report = new Report
            {
                Id = 1,
                Title = "R/1/2020",
                Date = new DateTime(2020, 1, 1, 12, 0, 0),
                Positions = new List<ReportPosition>
                {
                    new ReportPosition
                    {
                        Id = 1,
                        ReportId = 1,
                        Title = "Position 1",
                        Description = "Description 1",
                        Value = 43.01m
                    },
                    new ReportPosition
                    {
                        Id = 2,
                        ReportId = 1,
                        Title = "Position 2",
                        Description = "Description 2",
                        Value = 4311m
                    },
                    new ReportPosition
                    {
                        Id = 3,
                        ReportId = 1,
                        Title = "Position 3",
                        Description = "Description 3",
                        Value = 1.99m
                    }
                }
            };

            var errors = new List<Error>
            {
                new Error(){ Message = "Błąd testowy 1", Date = DateTime.Now},
                new Error(){ Message = "Błąd testowy 2", Date = DateTime.Now}
            };

            Console.WriteLine("Wysyłanie e-mail (raport dobowy)...");

            email.Send("Raport dobowy", htmlEmail.GenerateReport(report), emailReceiver).Wait();

            Console.WriteLine("Wysłano e-mail (raport dobowy)...");

            Console.WriteLine("Wysyłanie e-mail (błędy w aplikacji)...");

            email.Send("Błędy w aplikacji", htmlEmail.GenerateErrors(errors, 10), emailReceiver).Wait();

            Console.WriteLine("Wysłano e-mail (błędy w aplikacji)...");

        }
    }
}
