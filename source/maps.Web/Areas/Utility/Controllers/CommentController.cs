using maps.Model;
using maps.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace maps.Web.Areas.Utility.Controllers
{
    public class CommentController : UtilityController
    {
      
        public ActionResult Write(int id)
        {

            return View(new CommentView()
            {
                OwnerID = id
            });
        }

        [HttpPost]
        public ActionResult Write(CommentView commentView)
        {
            if (CurrentUser != null)
            {
                if (ModelState.IsValid)
                {
                    var comment = (Comment)ModelMapper.Map(commentView, typeof(CommentView), typeof(Comment));
                    comment.UserID = CurrentUser.ID;
                    comment.ID = 0;
                    Repository.CreateComment(comment);
                    var utilityIssueComment = new UtilityIssueComment
                    {
                        UtilityIssueID = commentView.OwnerID,
                        CommentID = comment.ID
                    };
                    Repository.CreateUtilityIssueComment(utilityIssueComment);
                    return View("_Ok");
                }
                return View(commentView);
            }
            return null;
        }
	}
}