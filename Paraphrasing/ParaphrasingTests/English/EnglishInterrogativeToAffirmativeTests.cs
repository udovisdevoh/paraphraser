using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Paraphrasing.Tests.English
{
    public class EnglishInterrogativeToAffirmativeTests
    {
        [Theory]
        [InlineData("Ain't it about time?", "It ain't about time.")]
        [InlineData("Aint you the guy with the master plan?", "You ain't the guy with the master plan.")]
        [InlineData("Am I the one?", "I am the one.")]
        [InlineData("Any last words to say?", "There are last words to say.")]
        [InlineData("Anybody ever say no?", "Some people say no.")]
        [InlineData("Aren't we all to you just near lost causes?", "We aren't all to you just near lost causes.")]
        [InlineData("Can't you see my pain?", "You can't see my pain.")]
        [InlineData("Canst thou not see the loss of loe painful is?", "You can't not see the loss of loe painful is.")]
        [InlineData("Cant you see what this does to me?", "You can't see what this does to me.")]
        [InlineData("Couldn't we just sit and share a smoke again?", "We couldn't just sit and share a smoke again.")]
        [InlineData("Did anybody coach you?", "Some people did coach you.")]
        [InlineData("Didn't I do a good job of pretending?", "I didn't do a good job of pretending.")]
        [InlineData("Didst thou unlock in silence of the deep?", "You did unlock in silence of the deep.")]
        [InlineData("Do angels have to depend on luck?", "Angels do have to depend on luck.")]
        [InlineData("Doesn't anybody stay in one place anymore?", "Some people doesn't stay in one place anymore.")]
        [InlineData("doest thou perceive thy desperate call from thy fathomless pits?", "You do perceive your desperate call from your fathomless pits.")]
        [InlineData("D'ya think we're on our own?", "You do think we're on our own.")]
        [InlineData("D'you know what she told me?", "You do know what she told me.")]
        [InlineData("got some guts to break free?", "Some got guts to break free.")]
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
        [InlineData("Will a wasteland remain?", "A wasteland will remain.")]
        [InlineData("Won't it make you glad when they're calling me crazy?", "It won't make you glad when they're calling me crazy.")]
        [InlineData("Wont you tell me what are we fighting for?", "You won't tell me what are we fighting for.")]
        [InlineData("Wouldn't it be a glorious day?", "It wouldn't be a glorious day.")]
        public void GivenInterrogativeSentence_ShouldBeDetectedRegardlessQuestionMark(string interrogativeSentence, string expectedAffirmativeSentence)
        {
            // Arrange
            EnglishInterrogativeToAffirmative englishInterrogativeToAffirmative = new EnglishInterrogativeToAffirmative();

            // Act
            string actualAffirmativeSentence = englishInterrogativeToAffirmative.Convert(interrogativeSentence);

            // Assert
            Assert.Equal(expectedAffirmativeSentence, actualAffirmativeSentence);
        }

        [Theory]
        [InlineData("Are all these feelings true?", "All these feelings are true")]
        [InlineData("Can a device read your mind?", "A device can read your mind.")]
        [InlineData("Could a dangerous toy be infallible?", "A dangerous toy could be infallible.")]
        [InlineData("Has all your self respect been lost?", "All your self respect has been lost.")]
        [InlineData("How long did you stay?", "You did stay long.")]
        [InlineData("How'd I get back here at this? Another cheap attempt at bliss", "I did get back here at this. Another cheap attempt at bliss")]
        [InlineData("How'm i supposed to be positive?", "I am supposed to be positive.")]
        [InlineData("Does any of this make sense?", "This does make sense.")]
        [InlineData("How's anybody supposed to love you baby 'til you do?", "Some people supposed to love you baby 'til you do.")]
        [InlineData("Wanna copy me and do exactly like I did?", "Copy me and do exactly like I did.")]
        [InlineData("Want a root beer?", "You want a root beer.")]
        [InlineData("Want beef?", "You want beef.")]
        [InlineData("Want me to freak ya?", "You want me to freak ya.")]
        [InlineData("Want to hear what he really is?", "You want to hear what he really is.")]
        [InlineData("Want us to tell 'em?", "You want us to tell 'em.")]
        [InlineData("Want your money back?", "You want your money back.")]
        [InlineData("Wha' happened?", "It happened.")]
        [InlineData("Wha'cha u wanna see us in five ten bedrooms?", "You do wanna see us in five ten bedrooms.")]
        [InlineData("Wha's up weezy baby?", "It is up weezy baby.")]
        [InlineData("What's up?", "It is up.")]
        [InlineData("What can be done that you haven't already stained in violence?", "It can be done that you haven't already stained in violence.")]
        [InlineData("Whatcha gonna do?", "I'm gonna do.")]
        [InlineData("What'll they say?", "They will say.")]
        [InlineData("What're you saying?", "You are saying.")]
        [InlineData("Whats left of the hope we have?", "It is left of the hope we have.")]
        [InlineData("When is enough better?", "Enough is better when.")]
        [InlineData("When's the last time you seen the rain?", "The last time you seen the rain.")]
        [InlineData("Where did all the good times go and why?", "All the good times did go because.")]
        [InlineData("Where'd all the good people go?", "All the good people did go.")]
        [InlineData("Where're my friends?", "My friends are.")]
        [InlineData("Where've you been hiding?", "You have been hiding.")]
        [InlineData("Which came first the music or the misery?", "The music came first.")]
        [InlineData("Who am I talking to?", "I am talking to.")]
        [InlineData("Who'd empty out my dustbins?", "Someone would empty out my dustbins.")]
        [InlineData("Who'll be around when the limelight's faded?", "Someone will be around when the limelight's faded.")]
        [InlineData("Whom does it care what do i need?", "It does care.")]
        [InlineData("Who's to blame for your low self esteem?", "Someone is to blame for your low self esteem.")]
        [InlineData("whose banner will stand in victory ?", "Someone's banner will stand in victory.")]
        [InlineData("Whut the dead like?", "The dead is like.")]
        [InlineData("Why do you think the poor loved him so much?", "Do you do think the poor loved him so much because.")]
        [InlineData("Would this song live on forever?", "This song would live on forever.")]
        public void GivenInterrogativeSentence_ShouldBeDetectedRegardlessQuestionMarkAdvanced(string interrogativeSentence, string expectedAffirmativeSentence)
        {
            // Arrange
            EnglishInterrogativeToAffirmative englishInterrogativeToAffirmative = new EnglishInterrogativeToAffirmative();

            // Act
            string actualAffirmativeSentence = englishInterrogativeToAffirmative.Convert(interrogativeSentence);

            // Assert
            Assert.Equal(expectedAffirmativeSentence, actualAffirmativeSentence);
        }
    }
}
