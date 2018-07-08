using MarkovMatrices;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Paraphrasing.Tests
{
    public class YodaInterrogativeToAffirmativeTests
    {
        private const string languageMatrixFileName = "./english.word.matrix.interrogation.only.bin"; // 50 fail

        #warning Uncomment unit tests

        [Theory]
        [InlineData("Ain't it about time?", "about time, it ain't.")]
        [InlineData("Aint you the guy with the master plan?", "The guy with the master plan, you ain't.")]
        [InlineData("Am I the one?", "The one, I am.")]
        [InlineData("Aren't we all to you just near lost causes?", "All to you just near lost causes, we aren't.")]
        [InlineData("Can't you see my pain?", "See my pain, you can't.")]
        [InlineData("Canst thou not see the loss of loe painful is?", "See the loss of loe painful is, you can not.")]
        [InlineData("Cant you see what this does to me?", "See what this does to me, you can't.")]
        [InlineData("Couldn't we just sit and share a smoke again?", "Just sit and share a smoke again, we couldn't.")]
        [InlineData("Didn't I do a good job of pretending?", "A good job of pretending, I didn't do.")]
        [InlineData("Didst thou unlock in silence of the deep?", "Unlock in silence of the deep, you did.")]
        [InlineData("Do angels have to depend on luck?", "Have to depend on luck, angels do.")]
        [InlineData("Doest thou perceive thy desperate call from thy fathomless pits?", "Perceive your desperate call from your fathomless pits, you do.")]
        [InlineData("D'ya think we're on our own?", "Think we're on our own, you do.")]
        [InlineData("D'you know what she told me?", "Know what she told me, you do.")]
        [InlineData("Had it crossed your mind that your heroes are failures in the end?", "Crossed your mind that your heroes are failures in the end, it had.")]
        [InlineData("Have I already tasted my piece of one sweet love?", "Already tasted my piece of one sweet love, I have.")]
        [InlineData("Haven't I seen you here before ?", "Seen you here before, I haven't.")]
        [InlineData("Is it right to bury it?", "Right to bury it, it is.")]
        [InlineData("Isn't everybody tired of the fighting?", "Tired of the fighting, everybody isn't.")]
        [InlineData("May I have this dance with you?", "Have this dance with you, I may.")]
        [InlineData("Must I always be?", "Always be, I must.")]
        [InlineData("Shall I call her?", "Call her, I will.")]
        [InlineData("Should I be feeling this loneliness?", "Feeling this loneliness, I should be.")]
        [InlineData("Shouldn't we jump in?", "Jump in, we shouldn't.")]
        [InlineData("Was she a vision that he created?", "A vision that he created, she was.")]
        [InlineData("Wasn't it just my choice to make?", "Just my choice to make, it wasn't.")]
        [InlineData("Were there people there?", "People there, there were.")]
        [InlineData("Weren't you the pilot who swerved off the course?", "The pilot who swerved off the course, you weren't.")]
        [InlineData("Won't it make you glad when they're calling me crazy?", "Make you glad when they're calling me crazy, it won't.")]
        [InlineData("Wont you tell me what are we fighting for?", "Tell me what are we fighting for, you won't.")]
        [InlineData("Wouldn't it be a glorious day?", "A glorious day, it wouldn't be.")]
        [InlineData("Wanna copy me and exactly like I did?", "Exactly like I did, copy me.")]
        public void GivenInterrogativeSentence_ShouldConvertShortSwap(string interrogativeSentence, string expectedAffirmativeSentence)
        {
            // Arrange
            EnglishInterrogativeToAffirmative englishInterrogativeToAffirmative = new EnglishInterrogativeToAffirmative(this.BuildWordOrderSwapper());

            // Act
            string actualAffirmativeSentence = englishInterrogativeToAffirmative.Convert(interrogativeSentence);

            // Assert
            Assert.Equal(expectedAffirmativeSentence.ToLowerInvariant(), actualAffirmativeSentence.ToLowerInvariant());
        }

        [Theory]
        [InlineData("Any last words to say?", "Last words to say, there are.")]
        /*[InlineData("Anybody ever say no?", "Somebody say no.")]
        [InlineData("Did anybody coach you?", "Somebody did coach you.")]
        [InlineData("Doesn't anybody stay in one place anymore?", "Somebody doesn't stay in one place anymore.")]
        [InlineData("Got some guts to break free?", "You do have some guts to break free.")]
        [InlineData("How'm i supposed to be positive?", "I am supposed to be positive.")]
        [InlineData("Why do you think the poor loved him so much?", "You do think the poor loved him so much.")]
        [InlineData("What'll they say?", "They will say.")]
        [InlineData("What're you saying?", "You are saying.")]
        [InlineData("How'd I get back here at this?", "I did get back here at this.")]
        [InlineData("Want me to freak ya?", "You do want me to freak ya.")]
        [InlineData("Want a root beer?", "You do want a root beer.")]
        [InlineData("Want beef?", "You do want beef.")]
        [InlineData("Want to hear what he really is?", "You do want to hear what he really is.")]
        [InlineData("Want us to tell 'em?", "You do want us to tell 'em.")]
        [InlineData("Want your money back?", "You do want your money back.")]
        [InlineData("How's anybody supposed to love you baby 'til you do?", "Somebody is supposed to love you baby 'til you do.")]
        [InlineData("Whatcha gonna do?", "You are gonna do.")]
        [InlineData("Where've you been hiding?", "You have been hiding.")]
        [InlineData("Who am I talking to?", "I am talking to.")]*/
        public void GivenInterrogativeSentence_ShouldConvertReplaceWordGroup(string interrogativeSentence, string expectedAffirmativeSentence)
        {
            // Arrange
            EnglishInterrogativeToAffirmative englishInterrogativeToAffirmative = new EnglishInterrogativeToAffirmative(this.BuildWordOrderSwapper());

            // Act
            string actualAffirmativeSentence = englishInterrogativeToAffirmative.Convert(interrogativeSentence);

            // Assert
            Assert.Equal(expectedAffirmativeSentence.ToLowerInvariant(), actualAffirmativeSentence.ToLowerInvariant());
        }

        /*
        [Theory]
        [InlineData("Are all these feelings true?", "All these feelings are true.")]
        [InlineData("Can a device read your mind?", "A device can read your mind.")]
        [InlineData("Could a dangerous toy be infallible?", "A dangerous toy could be infallible.")]
        [InlineData("Has all your self respect been lost?", "All your self respect has been lost.")]
        [InlineData("Will a wasteland remain?", "A wasteland will remain.")]
        [InlineData("Would this song live on forever?", "This song would live on forever.")]
        [InlineData("Whut is the dead like?", "The dead is like.")]
        [InlineData("Where'd all the good people go?", "All the good people did go.")]
        [InlineData("Where're my friends?", "My friends are.")]
        public void GivenInterrogativeSentence_ShouldConvertLongSwap(string interrogativeSentence, string expectedAffirmativeSentence)
        {
            // Arrange
            EnglishInterrogativeToAffirmative englishInterrogativeToAffirmative = new EnglishInterrogativeToAffirmative(this.BuildWordOrderSwapper());

            // Act
            string actualAffirmativeSentence = englishInterrogativeToAffirmative.Convert(interrogativeSentence);

            // Assert
            Assert.Equal(expectedAffirmativeSentence.ToLowerInvariant(), actualAffirmativeSentence.ToLowerInvariant());
        }

        [Theory]
        [InlineData("How long did you stay?", "You did stay long.")]
        [InlineData("Does any of this make sense?", "This does make sense.")]
        [InlineData("Wha' happened?", "It happened.")]
        [InlineData("Wha'cha u wanna see us in five ten bedrooms?", "You do wanna see us in five ten bedrooms.")]
        [InlineData("Wha's up weezy baby?", "It is up weezy baby.")]
        [InlineData("What's up?", "It is up.")]
        [InlineData("What can be done that you haven't already stained in violence?", "It can be done that you haven't already stained in violence.")]
        [InlineData("Whats left of the hope we have?", "It is left of the hope we have.")]
        [InlineData("When is enough better?", "Enough is better when.")]
        [InlineData("When's the last time you seen the rain?", "The last time you seen the rain is when.")]
        [InlineData("Where did all the good times go and why?", "All the good times did go.")]
        [InlineData("Which came first the music or the misery?", "The music came first.")]
        [InlineData("Who'd empty out my dustbins?", "Someone would empty out my dustbins.")]
        [InlineData("Who'll be around when the limelight's faded?", "Someone will be around when the limelight's faded.")]
        [InlineData("Whom does it care what do i need?", "It does care.")]
        [InlineData("Who's to blame for your low self esteem?", "Someone is to blame for your low self esteem.")]
        [InlineData("whose banner will stand in victory ?", "Someone's banner will stand in victory.")]
        public void GivenInterrogativeSentence_ShouldConvertComplex(string interrogativeSentence, string expectedAffirmativeSentence)
        {
            // Arrange
            EnglishInterrogativeToAffirmative englishInterrogativeToAffirmative = new EnglishInterrogativeToAffirmative(this.BuildWordOrderSwapper());

            // Act
            string actualAffirmativeSentence = englishInterrogativeToAffirmative.Convert(interrogativeSentence);

            // Assert
            Assert.Equal(expectedAffirmativeSentence, actualAffirmativeSentence);
        }
        */

        private IWordOrderSwapper BuildWordOrderSwapper()
        {
            //return ParaphrasingTestHelper.BuildWordOrderSwapperByMatrix(languageMatrixFileName);
            //return ParaphrasingTestHelper.BuildWordOrderSwapper();
            return ParaphrasingTestHelper.BuildYodaWordOrderSwapper();
        }
    }
}
