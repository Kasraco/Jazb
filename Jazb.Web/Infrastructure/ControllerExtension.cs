using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Jazb.Web
{
    public static class  ControllerExtension
    {
        public static void ErrorMessage(this Controller controller,string Message,string Title="",bool Plus=false)
        {

            string NewMessage = Notification.Show(message: Message, title: Title, position: Position.BottomRight, type: ToastType.Error);
            string OldMessage= controller.TempData["Message"] as string;
            if (Plus)
                OldMessage = OldMessage + NewMessage;
            else
                OldMessage = NewMessage;

            controller.TempData["Message"] = OldMessage;
            return;
        }

        public static void SuccessMessage(this Controller controller, string Message, string Title = "", bool Plus = false)
        {

            string NewMessage = Notification.Show(message: Message, title: Title,  position: Position.BottomRight, type: ToastType.Success);
            string OldMessage = controller.TempData["Message"] as string;
            if (Plus)
                OldMessage = OldMessage + NewMessage;
            else
                OldMessage = NewMessage;

            controller.TempData["Message"] = OldMessage;
            return;
        }

        public static void InfoMessage(this Controller controller, string Message, string Title = "", bool Plus = false)
        {

            string NewMessage = Notification.Show(message: Message, title: Title,  position: Position.BottomRight, type: ToastType.Info);
            string OldMessage = controller.TempData["Message"] as string;
            if (Plus)
                OldMessage = OldMessage + NewMessage;
            else
                OldMessage = NewMessage;

            controller.TempData["Message"] = OldMessage;
            return;
        }


        public static void WarningMessage(this Controller controller, string Message, string Title = "", bool Plus = false)
        {

            string NewMessage = Notification.Show(message: Message, title: Title,  position: Position.BottomRight, type: ToastType.Warning);
            string OldMessage = controller.TempData["Message"] as string;
            if (Plus)
                OldMessage = OldMessage + NewMessage;
            else
                OldMessage = NewMessage;

            controller.TempData["Message"] = OldMessage;
            return;
        }

    }
}