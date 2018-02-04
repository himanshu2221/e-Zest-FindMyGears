using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FindMyGears.Model;
using FindMyGears.Utility;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace FindMyGears.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {

        private List<Questionary> questionaryList;
        private Helper helper;

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(StartConversationAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            // calculate something for us to return
            int length = (activity.Text ?? string.Empty).Length;

            // return our reply to the user
            await context.PostAsync($"You sent {activity.Text} which was {length} characters");

            context.Wait(MessageReceivedAsync);
        }

        private async Task StartConversationAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result as Activity;

            //Activity reply = activity.CreateReply($"Welcome in Find My Gears Bot!");
            Activity reply = activity.CreateReply();


            CardAction Running = new CardAction()
            {
                Value = "1",
                Type = "postBack",
                Title = "Running",
            };


            CardAction Cricket = new CardAction()
            {
                Value = "2",
                Type = "postBack",
                Title = "Cricket",
            };


            var welcomeCard = new ThumbnailCard
            {
                Title = "Welcomde in Find My Gears Bot!",
                Text = "FindMyGears Bot provides e-Commerce Platform to help you to buy Sports goods as per your requirement",
                Images = new List<CardImage> { new CardImage("https://www.minnetonkaschools.org/uploaded/photos/07/15-16/MHS/Athletics/Basketball/FallSportsIcon.png") },
                Buttons = new List<CardAction> { Running, Cricket }
            };

            Attachment plAttachment = welcomeCard.ToAttachment();
            reply.Attachments.Add(plAttachment);
            await context.PostAsync(reply);
            context.Wait(Selection);
        }

        private async Task Selection(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result as Activity;

            //User Selection
            if (activity.Text == "1")
            {
                Activity reply = ShowSubCategories(context, result, activity);
                await context.PostAsync(reply);
                context.Wait(ShowResult);
            }



        }


        private Activity ShowSubCategories(IDialogContext context, IAwaitable<IMessageActivity> result, Activity activity)
        {
            helper = new Helper();
            List<SubCategory> subCategoryList = helper.ShowCategories();
            // var activity = await result as Activity;

            Activity reply = activity.CreateReply();
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;

            foreach (SubCategory subCategory in subCategoryList)
            {
                reply.Attachments.Add(CreateCard(subCategory.ImageUrl, subCategory.SubCategoryName));
            }

            return reply;


        }

        private async Task ShowResult(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result as Activity;
            Activity reply = activity.CreateReply();

            //Ask questions to search for exact requirement --It will be Done by Machine Learning
            //questionaryList = helper.GetQuestionaryList();
            //int id = helper.GetId(activity.Text);

            //foreach(Questionary questionary in questionaryList)
            //{
            //    if (questionary.Id == id)
            //    {
            Attachment attachment = CreateThumbnailCard(Questionary.genderQuestion);
            reply.Attachments.Add(attachment);

            //    }
            //}

            await context.PostAsync(reply);
            context.Wait(Selection);
        }

        private Attachment CreateCard(String imageUrl, String value)
        {
            List<CardAction> cardButtons = new List<CardAction>();
            List<CardImage> cardImages = new List<CardImage>();
            cardImages.Add(new CardImage(imageUrl));

            CardAction plButton = new CardAction()
            {
                Value = value,
                Type = ActionTypes.PostBack,
                Title = value
            };

            cardButtons.Add(plButton);

            HeroCard plCard = new HeroCard()
            {
                Images = cardImages,
                Buttons = cardButtons
            };


            Attachment attachment = new Attachment()
            {
                ContentType = HeroCard.ContentType,
                Content = plCard
            };

            return attachment;
        }

        private Attachment CreateThumbnailCard(string questionary)
        {
            CardAction firstCard = new CardAction()
            {
                Value = "1",
                Type = "postBack",
                Title = Constants.male
            };


            CardAction secondCard = new CardAction()
            {
                Value = "2",
                Type = "postBack",
                Title = Constants.female
            };


            var card = new ThumbnailCard
            {
                Title = questionary,
                Buttons = new List<CardAction> { firstCard, secondCard }
            };

            Attachment attachment = card.ToAttachment();
            return attachment;
        }
    }
}