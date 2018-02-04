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

        //private List<Questionary> questionaryList;
        private Helper helper;
        private UserProfile userProfile;
        private OrderPayLoad orderPayLoad;

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(StartConversationAsync);

            return Task.CompletedTask;
        }
        //   await context.PostAsync($"You sent {activity.Text} which was {length} characters");


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
                context.Wait(GetAge);
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

        //Ask questions to search for exact requirement -- Searching will be Done by Machine Learning
        private async Task GetName(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result as Activity;
            Activity reply = activity.CreateReply();

            await context.PostAsync(reply);
            context.Wait(GetAge);
        }

        private async Task GetAge(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            userProfile = new UserProfile();
            var activity = await result as Activity;
            Activity reply = activity.CreateReply();
            userProfile.Name = activity.Text;

            CardAction firstCard = new CardAction()
            {
                Value = "1",
                Type = "postBack",
                Title = Constants.firstAgeGroup
            };


            CardAction secondCard = new CardAction()
            {
                Value = "2",
                Type = "postBack",
                Title = Constants.secondAgeGroup
            };

            CardAction thirdCard = new CardAction()
            {
                Value = "3",
                Type = "postBack",
                Title = Constants.thirdAgeGroup
            };


            CardAction fourthCard = new CardAction()
            {
                Value = "4",
                Type = "postBack",
                Title = Constants.fourthAgeGroup
            };



            var card = new ThumbnailCard
            {
                Title = Questionary.ageQuestion,
                Buttons = new List<CardAction> { firstCard, secondCard, thirdCard, fourthCard }
            };

            Attachment attachment = card.ToAttachment();

            context.Wait(GetGender);
        }

        private async Task GetGender(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result as Activity;
            Activity reply = activity.CreateReply();
            userProfile.Age = activity.Text;

            Attachment attachment = CreateThumbnailCard(Questionary.genderQuestion, Constants.male, Constants.female);
            reply.Attachments.Add(attachment);

            await context.PostAsync(reply);
            context.Wait(GetSize);
        }

        private async Task GetSize(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            orderPayLoad = new OrderPayLoad();
            var activity = await result as Activity;
            Activity reply = activity.CreateReply();
            userProfile.Gender = activity.Text;

            //Questionary.sizeQuestion
            await context.PostAsync(Questionary.sizeQuestion);


            context.Wait(GetRunningType);
        }

        private async Task GetRunningType(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result as Activity;
            Activity reply = activity.CreateReply();
            orderPayLoad.Size = activity.Text;

            Attachment attachment = CreateThumbnailCard(Questionary.runningTypeQuestion, "On Road", "On TrackMill");
            reply.Attachments.Add(attachment);

            await context.PostAsync(reply);
            context.Wait(Next);
        }

        private async Task Next(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result as Activity;
            Activity reply = activity.CreateReply($"As per your need, the following are the recommendations");
            orderPayLoad.RunningType = activity.Text;


            // helper = new Helper();
            List<Products> productList = helper.GetProducts();

            //Activity reply = activity.CreateReply();
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;

            foreach (Products product in productList)
            {
                reply.Attachments.Add(CreateCard(product.ImageUrl, product.Price));
            }

            await context.PostAsync(reply);
            context.Wait(OrderPlaced);
        }

        private async Task OrderPlaced(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result as Activity;
            Activity reply = activity.CreateReply($"Your Order has been Placed. You will recieve confirmation email from us.");
            await context.PostAsync(reply);
            context.Wait(StartConversationAsync);
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

        private Attachment CreateThumbnailCard(string questionary, string firstTitle, string secondTitle)
        {
            CardAction firstCard = new CardAction()
            {
                Value = "1",
                Type = "postBack",
                Title = firstTitle
            };


            CardAction secondCard = new CardAction()
            {
                Value = "2",
                Type = "postBack",
                Title = secondTitle
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