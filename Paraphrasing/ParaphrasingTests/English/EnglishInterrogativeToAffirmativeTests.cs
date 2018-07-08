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
    public class EnglishInterrogativeToAffirmativeTests
    {
        private const string languageMatrixFileName = "./english.word.matrix.interrogation.only.bin"; // 50 fail

        #warning Uncomment unit tests

        [Theory]
        [InlineData("Ain't it about time?", "about time, it ain't.")]
        [InlineData("Aint you the guy with the master plan?", "The guy with the master plan, you ain't.")]
        [InlineData("Am I the one?", "The one, I am.")]
        [InlineData("Aren't we all to you just near lost causes?", "All to you just near lost causes, we aren't.")]
        [InlineData("Can't you see my pain?", "See my pain, you can't.")]
        [InlineData("Canst thou not see the loss of loe painful is?", "You can not see the loss of loe painful is.")]
        /*[InlineData("Cant you see what this does to me?", "You can't see what this does to me.")]
        [InlineData("Couldn't we just sit and share a smoke again?", "We couldn't just sit and share a smoke again.")]
        [InlineData("Didn't I do a good job of pretending?", "I didn't do a good job of pretending.")]
        [InlineData("Didst thou unlock in silence of the deep?", "You did unlock in silence of the deep.")]
        [InlineData("Do angels have to depend on luck?", "Angels do have to depend on luck.")]
        [InlineData("Doest thou perceive thy desperate call from thy fathomless pits?", "You do perceive your desperate call from your fathomless pits.")]
        [InlineData("D'ya think we're on our own?", "You do think we're on our own.")]
        [InlineData("D'you know what she told me?", "You do know what she told me.")]
        [InlineData("Had it crossed your mind that your heroes are failures in the end?", "It had crossed your mind that your heroes are failures in the end.")]
        [InlineData("Have I already tasted my piece of one sweet love?", "I have already tasted my piece of one sweet love.")]
        [InlineData("Haven't I seen you here before ?", "I haven't seen you here before.")]
        [InlineData("Is it right to bury it?", "It is right to bury it.")]
        [InlineData("Isn't everybody tired of the fighting?", "Everybody isn't tired of the fighting.")]
        [InlineData("May I have this dance with you?", "I may have this dance with you.")]
        [InlineData("Must I always be?", "I must always be.")]
        [InlineData("Shall I call her?", "I will call her.")]
        [InlineData("Should I be feeling this loneliness?", "I should be feeling this loneliness.")]
        [InlineData("Shouldn't we jump in?", "We shouldn't jump in.")]
        [InlineData("Was she a vision that he created?", "She was a vision that he created.")]
        [InlineData("Wasn't it just my choice to make?", "It wasn't just my choice to make.")]
        [InlineData("Were there people there?", "There were people there.")]
        [InlineData("Weren't you the pilot who swerved off the course?", "You weren't the pilot who swerved off the course.")]
        [InlineData("Won't it make you glad when they're calling me crazy?", "It won't make you glad when they're calling me crazy.")]
        [InlineData("Wont you tell me what are we fighting for?", "You won't tell me what are we fighting for.")]
        [InlineData("Wouldn't it be a glorious day?", "It wouldn't be a glorious day.")]
        [InlineData("Wanna copy me and exactly like I did?", "Copy me and exactly like I did.")]*/
        public void GivenInterrogativeSentence_ShouldConvertShortSwap(string interrogativeSentence, string expectedAffirmativeSentence)
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
        [InlineData("Any last words to say?", "There are last words to say.")]
        [InlineData("Anybody ever say no?", "Somebody say no.")]
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
        [InlineData("Who am I talking to?", "I am talking to.")]
        public void GivenInterrogativeSentence_ShouldConvertReplaceWordGroup(string interrogativeSentence, string expectedAffirmativeSentence)
        {
            // Arrange
            EnglishInterrogativeToAffirmative englishInterrogativeToAffirmative = new EnglishInterrogativeToAffirmative(this.BuildWordOrderSwapper());

            // Act
            string actualAffirmativeSentence = englishInterrogativeToAffirmative.Convert(interrogativeSentence);

            // Assert
            Assert.Equal(expectedAffirmativeSentence.ToLowerInvariant(), actualAffirmativeSentence.ToLowerInvariant());
        }

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
