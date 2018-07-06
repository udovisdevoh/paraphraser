using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Paraphrasing.Tests
{
    public class EnglishSentenceTypeDetectorInterrogativeTests
    {
        [Theory]
        [InlineData("ain't it about time they looked for other ways of how to beat the problems?")]
        [InlineData("aint you the guy with the master plan?")]
        [InlineData("am I the one?")]
        [InlineData("any last words to say? this is your final day")]
        [InlineData("anybody ever say no?")]
        [InlineData("are all these feelings true?")]
        [InlineData("aren't we all to you just near lost causes?")]
        [InlineData("can a device read your mind?")]
        [InlineData("can't you see my pain?")]
        [InlineData("canst thou not see the loss of loe painful is?")]
        [InlineData("cant you see what this does to me?")]
        [InlineData("could a dangerous toy be infallible?")]
        [InlineData("couldn't we just sit and share a smoke again?")]
        [InlineData("did anybody coach you?")]
        [InlineData("didn't i do a good job of pretending?")]
        [InlineData("didst thou unlock in silence of the deep?")]
        [InlineData("do angels have to depend on luck?")]
        [InlineData("does any of this make fucking sense?")]
        [InlineData("doesn't anybody stay in one place anymore?")]
        [InlineData("doest thou perceive ye desperate call from ye fathomless pits?")]
        [InlineData("d'ya think we're on our own?")]
        [InlineData("d'you know what she told me?")]
        [InlineData("got some guts to break free?")]
        [InlineData("had it crossed your mind that your heroes are failures in the end?")]
        [InlineData("has all your self respect been lost?")]
        [InlineData("have i already tasted my piece of one sweet love?")]
        [InlineData("haven't i seen you here before ?")]
        [InlineData("how long lived i waiting?")]
        [InlineData("how'd i get back here at this? another cheap attempt at bliss")]
        [InlineData("how'm i 'sposed to be positive when i don't see shit positive?")]
        [InlineData("how's anybody supposed to love you baby 'til you do?")]
        [InlineData("is do we bury him or burn? any suggestions?")]
        [InlineData("isn't everybody tired of the fighting? hey hey")]
        [InlineData("may i have this dance with you?")]
        [InlineData("must i always be playing playing your fool?")]
        [InlineData("shall i call her good when she proves unkind?")]
        [InlineData("should i be feeling this loneliness?")]
        [InlineData("shouldn't we jump in?")]
        [InlineData("wanna copy me and do exactly like i did? yeah yeah")]
        [InlineData("want a root beer? hell no")]
        [InlineData("want beef? i'll do like summertime and raise up the heat")]
        [InlineData("want me to freak ya? wait for me to skeet ya")]
        [InlineData("want to hear what he really is?")]
        [InlineData("want us to kill him for you? no i got some pills you want 'em?")]
        [InlineData("want your family back? let's get it")]
        [InlineData("was she a vision that he created?")]
        [InlineData("wasn't it just my choice to make?")]
        [InlineData("were there people there?")]
        [InlineData("weren't you the pilot who swerved off the course?")]
        [InlineData("wha' happened? wha' happened to my friend?")]
        [InlineData("wha'cha like man wha'cha u wanna see us in five ten bedrooms?")]
        [InlineData("wha's up weezy baby?")]
        [InlineData("what's up?")]
        [InlineData("what can be done that you haven't already stained in violence?")]
        [InlineData("whatcha gonna do if it gets to weary through the horn you not gonna exit?")]
        [InlineData("what'll they say? that's punk rock")]
        [InlineData("what're you saying? just tell me now")]
        [InlineData("whats left of the hope we have?")]
        [InlineData("when is enough enough?")]
        [InlineData("when's the last time you seen the rain?")]
        [InlineData("where did all the good times go and why?")]
        [InlineData("where'd all the good people go?")]
        [InlineData("where're my niggers living better?")]
        [InlineData("where've you been hiding?")]
        [InlineData("which came first the music or the misery?")]
        [InlineData("who am i talking to?")]
        [InlineData("who'd empty out my dustbins? would i still get plastic bags?")]
        [InlineData("who'll be around when the limelight's faded?")]
        [InlineData("whom does it care what do i need?")]
        [InlineData("who's to blame for your low self esteem?")]
        [InlineData("whose banner will stand in victory ?")]
        [InlineData("whut the dead like? fuck the spotlight")]
        [InlineData("why do you think the poor loved him so much?")]
        [InlineData("will a wasteland remain?")]
        [InlineData("won't it make you glad when they're calling me crazy?")]
        [InlineData("wont you tell me what are we fighting for?")]
        [InlineData("would this song live on forever?")]
        [InlineData("wouldn't it be a glorious day?")]
        public void GivenInterrogativeSentence_ShouldBeDetectedRegardlessQuestionMark(string sentence)
        {
            // Arrange
            sentence = sentence.Replace("?", "");
            SentenceTypeDetector sentenceDetector = new EnglishSentenceTypeDetector();
            SentenceType expectedSentenceType = SentenceType.Interrogative;

            // Act
            SentenceType actualSentenceType = sentenceDetector.GetSentenceType(sentence);

            // Assert
            if (expectedSentenceType != actualSentenceType)
            {
                Assert.Equal("should be interrogative sentence", sentence); // hack pour afficher dans la console de xUnit
            }
            Assert.Equal(expectedSentenceType, actualSentenceType);
        }
    }
}
