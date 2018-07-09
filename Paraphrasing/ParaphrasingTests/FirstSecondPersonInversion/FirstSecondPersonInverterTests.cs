using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Paraphrasing.Tests
{
    public class FirstSecondPersonInverterTests
    {
        #region From I to You
        [Theory]
        [InlineData("I say this is great.", "You say this is great.")]
        [InlineData("Do I say this is great.", "Do you say this is great.")]
        [InlineData("Steve and I are going.", "Steve and you are going.")]
        public void GivenSentenceWithI_ShouldConvertToYou(string input, string expectedOutput)
        {
            // Arrange
            FirstSecondPersonInverter firstSecondPersonInverter = new FirstSecondPersonInverter();

            // Act
            string actualOutput = firstSecondPersonInverter.Convert(input);

            // Assert
            Assert.Equal(expectedOutput.ToLowerInvariant(), actualOutput.ToLowerInvariant());
        }

        [Theory]
        [InlineData("I am great.", "You are great.")]
        [InlineData("Steve says I am great.", "Steve says you are great.")]
        public void GivenSentenceWithIam_ShouldConvertToYouAre(string input, string expectedOutput)
        {
            // Arrange
            FirstSecondPersonInverter firstSecondPersonInverter = new FirstSecondPersonInverter();

            // Act
            string actualOutput = firstSecondPersonInverter.Convert(input);

            // Assert
            Assert.Equal(expectedOutput.ToLowerInvariant(), actualOutput.ToLowerInvariant());
        }

        [Theory]
        [InlineData("I'm great.", "You're great.")]
        [InlineData("Steve says I'm great.", "Steve says you're great.")]
        public void GivenSentenceWithIm_ShouldConvertToYouRe(string input, string expectedOutput)
        {
            // Arrange
            FirstSecondPersonInverter firstSecondPersonInverter = new FirstSecondPersonInverter();

            // Act
            string actualOutput = firstSecondPersonInverter.Convert(input);

            // Assert
            Assert.Equal(expectedOutput.ToLowerInvariant(), actualOutput.ToLowerInvariant());
        }

        [Theory]
        [InlineData("I was great.", "You were great.")]
        [InlineData("Steve says I was great.", "Steve says you were great.")]
        public void GivenSentenceWithIWas_ShouldConvertToYouWere(string input, string expectedOutput)
        {
            // Arrange
            FirstSecondPersonInverter firstSecondPersonInverter = new FirstSecondPersonInverter();

            // Act
            string actualOutput = firstSecondPersonInverter.Convert(input);

            // Assert
            Assert.Equal(expectedOutput.ToLowerInvariant(), actualOutput.ToLowerInvariant());
        }
        #endregion

        #region From Me to You
        [Theory]
        [InlineData("Steve told me how great it is.", "Steve told you how great it is.")]
        [InlineData("How could he say that to me?", "How could he say that to you?")]
        public void GivenSentenceWithMe_ShouldConvertToYou(string input, string expectedOutput)
        {
            // Arrange
            FirstSecondPersonInverter firstSecondPersonInverter = new FirstSecondPersonInverter();

            // Act
            string actualOutput = firstSecondPersonInverter.Convert(input);

            // Assert
            Assert.Equal(expectedOutput.ToLowerInvariant(), actualOutput.ToLowerInvariant());
        }
        #endregion

        #region From My to Your
        [Theory]
        [InlineData("This is my car.", "This is your car.")]
        public void GivenSentenceWithMy_ShouldConvertToYour(string input, string expectedOutput)
        {
            // Arrange
            FirstSecondPersonInverter firstSecondPersonInverter = new FirstSecondPersonInverter();

            // Act
            string actualOutput = firstSecondPersonInverter.Convert(input);

            // Assert
            Assert.Equal(expectedOutput.ToLowerInvariant(), actualOutput.ToLowerInvariant());
        }
        #endregion

        #region From Mine to Yours
        [Theory]
        [InlineData("This car is mine.", "This car is yours.")]
        public void GivenSentenceWithMine_ShouldConvertToYours(string input, string expectedOutput)
        {
            // Arrange
            FirstSecondPersonInverter firstSecondPersonInverter = new FirstSecondPersonInverter();

            // Act
            string actualOutput = firstSecondPersonInverter.Convert(input);

            // Assert
            Assert.Equal(expectedOutput.ToLowerInvariant(), actualOutput.ToLowerInvariant());
        }
        #endregion

        #region From Myself to Yourself
        [Theory]
        [InlineData("Steve keeps it for myself.", "Steve keeps it for yourself.")]
        public void GivenSentenceWithMyself_ShouldConvertToYourself(string input, string expectedOutput)
        {
            // Arrange
            FirstSecondPersonInverter firstSecondPersonInverter = new FirstSecondPersonInverter();

            // Act
            string actualOutput = firstSecondPersonInverter.Convert(input);

            // Assert
            Assert.Equal(expectedOutput.ToLowerInvariant(), actualOutput.ToLowerInvariant());
        }
        #endregion

        #region From We to You
        [Theory]
        [InlineData("We will rock the place.", "You will rock the place.")]
        public void GivenSentenceWithWe_ShouldConvertToYou(string input, string expectedOutput)
        {
            // Arrange
            FirstSecondPersonInverter firstSecondPersonInverter = new FirstSecondPersonInverter();

            // Act
            string actualOutput = firstSecondPersonInverter.Convert(input);

            // Assert
            Assert.Equal(expectedOutput.ToLowerInvariant(), actualOutput.ToLowerInvariant());
        }

        [Theory]
        [InlineData("We're going to rock the place.", "You're going to rock the place.")]
        public void GivenSentenceWithWeRe_ShouldConvertToYouRe(string input, string expectedOutput)
        {
            // Arrange
            FirstSecondPersonInverter firstSecondPersonInverter = new FirstSecondPersonInverter();

            // Act
            string actualOutput = firstSecondPersonInverter.Convert(input);

            // Assert
            Assert.Equal(expectedOutput.ToLowerInvariant(), actualOutput.ToLowerInvariant());
        }
        #endregion

        #region From Us to You guys
        [Theory]
        [InlineData("He is one of us.", "He is one of you guys.")]
        public void GivenSentenceWithUs_ShouldNotConvert(string input, string expectedOutput)
        {
            // Arrange
            FirstSecondPersonInverter firstSecondPersonInverter = new FirstSecondPersonInverter();

            // Act
            string actualOutput = firstSecondPersonInverter.Convert(input);

            // Assert
            Assert.Equal(expectedOutput.ToLowerInvariant(), actualOutput.ToLowerInvariant());
        }
        #endregion

        #region From Our to Your
        [Theory]
        [InlineData("This is our car.", "This is your car.")]
        [InlineData("Our car is the best.", "Your car is the best.")]
        public void GivenSentenceWithOur_ShouldConvertToYour(string input, string expectedOutput)
        {
            // Arrange
            FirstSecondPersonInverter firstSecondPersonInverter = new FirstSecondPersonInverter();

            // Act
            string actualOutput = firstSecondPersonInverter.Convert(input);

            // Assert
            Assert.Equal(expectedOutput.ToLowerInvariant(), actualOutput.ToLowerInvariant());
        }
        #endregion

        #region From Ours to Yours
        [Theory]
        [InlineData("This car is ours.", "This car is yours.")]
        public void GivenSentenceWithOurs_ShouldConvertToYours(string input, string expectedOutput)
        {
            // Arrange
            FirstSecondPersonInverter firstSecondPersonInverter = new FirstSecondPersonInverter();

            // Act
            string actualOutput = firstSecondPersonInverter.Convert(input);

            // Assert
            Assert.Equal(expectedOutput.ToLowerInvariant(), actualOutput.ToLowerInvariant());
        }
        #endregion

        #region From Ourselves to Yourselves
        [Theory]
        [InlineData("Steve told that to ourselves.", "Steve told that to yourselves.")]
        public void GivenSentenceWithOurselves_ShouldConvertToYourselves(string input, string expectedOutput)
        {
            // Arrange
            FirstSecondPersonInverter firstSecondPersonInverter = new FirstSecondPersonInverter();

            // Act
            string actualOutput = firstSecondPersonInverter.Convert(input);

            // Assert
            Assert.Equal(expectedOutput.ToLowerInvariant(), actualOutput.ToLowerInvariant());
        }
        #endregion

        #region From You to Me
        // https://en.wikipedia.org/wiki/List_of_English_prepositions
        [Theory]
        [InlineData("Steve laughs at you.", "Steve laughs at me.")]
        [InlineData("Steve talks about you.", "Steve talks about me.")]
        [InlineData("Steve walks above you.", "Steve walks above me.")]
        [InlineData("Steve walks across you.", "Steve walks across me.")]
        [InlineData("Steve talks after you.", "Steve talks after me.")]
        [InlineData("Steve fights against you.", "Steve fights against me.")]
        [InlineData("Steve sings along you.", "Steve sings along me.")]
        [InlineData("Steve sings alongst you.", "Steve sings alongst me.")]
        [InlineData("Steve walks alongside you.", "Steve walks alongside me.")]
        [InlineData("Steve walks amid you.", "Steve walks amid me.")]
        [InlineData("Steve walks amidst you.", "Steve walks amidst me.")]
        [InlineData("Steve laughs among you.", "Steve laughs among me.")]
        [InlineData("Steve laughs amongst you.", "Steve laughs amongst me.")]
        [InlineData("Steve laughs 'mongst you.", "Steve laughs 'mongst me.")]
        [InlineData("Steve talks apud you.", "Steve talks apud me.")]
        [InlineData("Steve walks around you.", "Steve walks around me.")]
        [InlineData("Steve walks 'round you.", "Steve walks 'round me.")]
        [InlineData("The same as you.", "The same as me.")]
        [InlineData("Steve talks astride you.", "Steve talks astride me.")]
        [InlineData("Steve walks before you.", "Steve walks before me.")]
        [InlineData("Steve walks afore you.", "Steve walks afore me.")]
        [InlineData("Steve walks tofore you.", "Steve walks tofore me.")]
        [InlineData("Steve walks b4 you.", "Steve walks b4 me.")]
        [InlineData("Steve walks behind you.", "Steve walks behind me.")]
        [InlineData("Steve walks ahind you.", "Steve walks ahind me.")]
        [InlineData("Steve walks below you.", "Steve walks below me.")]
        [InlineData("Steve walks ablow you.", "Steve walks ablow me.")]
        [InlineData("Steve walks allow you.", "Steve walks allow me.")]
        [InlineData("Steve walks beneath you.", "Steve walks beneath me.")]
        [InlineData("Steve walks 'neath you.", "Steve walks 'neath me.")]
        [InlineData("Steve walks neath you.", "Steve walks neath me.")]
        [InlineData("Steve walks beside you.", "Steve walks beside me.")]
        [InlineData("Steve walks besides you.", "Steve walks besides me.")]
        [InlineData("Steve walks between you.", "Steve walks between me.")]
        [InlineData("Steve walks atween you.", "Steve walks atween me.")]
        [InlineData("Steve walks beyond you.", "Steve walks beyond me.")]
        [InlineData("Steve talks by you.", "Steve talks by me.")]
        [InlineData("Steve eats chez you.", "Steve eats chez me.")]
        [InlineData("Steve talks despite you.", "Steve talks despite me.")]
        [InlineData("Steve talks spite you.", "Steve talks spite me.")]
        [InlineData("Steve talks down you.", "Steve talks down me.")]
        [InlineData("Steve talks during you.", "Steve talks during me.")]
        [InlineData("Anyone except you.", "Anyone except me.")]
        [InlineData("Steve talks for you.", "Steve talks for me.")]
        [InlineData("Steve talks 4 you.", "Steve talks 4 me.")]
        [InlineData("Steve walks from you.", "Steve walks from me.")]
        [InlineData("Steve listens in you.", "Steve listens in me.")]
        [InlineData("Steve talks inside you.", "Steve talks inside me.")]
        [InlineData("Steve talks into you.", "Steve talks into me.")]
        [InlineData("Steve talks like you.", "Steve talks like me.")]
        [InlineData("Steve talks minus you.", "Steve talks minus me.")]
        [InlineData("Steve talks near you.", "Steve talks near me.")]
        [InlineData("Steve talks anear you.", "Steve talks anear me.")]
        [InlineData("Steve talks notwithstanding you.", "Steve talks notwithstanding me.")]
        [InlineData("Steve talks of you.", "Steve talks of me.")]
        [InlineData("Steve talks off you.", "Steve talks off me.")]
        [InlineData("Steve walks on you.", "Steve walks on me.")]
        [InlineData("Steve walks onto you.", "Steve walks onto me.")]
        [InlineData("Steve walks out you.", "Steve walks out me.")]
        [InlineData("Steve walks outen you.", "Steve walks outen me.")]
        [InlineData("Steve talks outside you.", "Steve talks outside me.")]
        [InlineData("Steve talks over you.", "Steve talks over me.")]
        [InlineData("Steve talks past you.", "Steve talks past me.")]
        [InlineData("Steve talks per you.", "Steve talks per me.")]
        [InlineData("Steve talks plus you.", "Steve talks plus me.")]
        [InlineData("Steve talks post you.", "Steve talks post me.")]
        [InlineData("Steve talks sans you.", "Steve talks sans me.")]
        [InlineData("Steve talks save you.", "Steve talks save me.")]
        [InlineData("Steve talks since you.", "Steve talks since me.")]
        [InlineData("Steve talks sithence you.", "Steve talks sithence me.")]
        [InlineData("Steve talks more than you.", "Steve talks more than me.")]
        [InlineData("Steve talks through you.", "Steve talks through me.")]
        [InlineData("Steve talks thru you.", "Steve talks thru me.")]
        [InlineData("Steve talks throughout you.", "Steve talks throughout me.")]
        [InlineData("Steve talks thruout you.", "Steve talks thruout me.")]
        [InlineData("Steve talks till you.", "Steve talks till me.")]
        [InlineData("Steve talks to you.", "Steve talks to me.")]
        [InlineData("Steve talks toward you.", "Steve talks toward me.")]
        [InlineData("Steve talks towards you.", "Steve talks towards me.")]
        [InlineData("Steve talks under you.", "Steve talks under me.")]
        [InlineData("Steve talks underneath you.", "Steve talks underneath me.")]
        [InlineData("Steve talks unlike you.", "Steve talks unlike me.")]
        [InlineData("Steve talks until you.", "Steve talks until me.")]
        [InlineData("Steve talks 'till you.", "Steve talks 'till me.")]
        [InlineData("Steve talks unto you.", "Steve talks unto me.")]
        [InlineData("Steve talks up you.", "Steve talks up me.")]
        [InlineData("Steve talks upon you.", "Steve talks upon me.")]
        [InlineData("Steve talks pon you.", "Steve talks pon me.")]
        [InlineData("Steve talks 'pon you.", "Steve talks 'pon me.")]
        [InlineData("Steve talks versus you.", "Steve talks versus me.")]
        [InlineData("Steve talks vs you.", "Steve talks vs me.")]
        [InlineData("Steve talks via you.", "Steve talks via me.")]
        [InlineData("Steve talks vis-à-vis you.", "Steve talks vis-à-vis me.")]
        [InlineData("Steve talks with you.", "Steve talks with me.")]
        [InlineData("Steve talks within you.", "Steve talks within me.")]
        [InlineData("Steve talks without you.", "Steve talks without me.")]
        [InlineData("Steves follows you.", "Steves follows me.")]
        [InlineData("Steves follows you, it is obvious.", "Steves follows me, it is obvious.")]
        [InlineData("Steves follows you and it is obvious.", "Steves follows me and it is obvious.")]
        public void GivenSentenceWithYou_ShouldConvertToMe(string input, string expectedOutput)
        {
            // Arrange
            FirstSecondPersonInverter firstSecondPersonInverter = new FirstSecondPersonInverter();

            // Act
            string actualOutput = firstSecondPersonInverter.Convert(input);

            // Assert
            Assert.Equal(expectedOutput.ToLowerInvariant(), actualOutput.ToLowerInvariant());
        }
        #endregion

        #region From You to I
        [Theory]
        [InlineData("You say this is great.", "I say this is great.")]
        [InlineData("Do you say this is great.", "Do I say this is great.")]
        [InlineData("Steve and you are going.", "Steve and I are going.")]
        public void GivenSentenceWithYou_ShouldConvertToI(string input, string expectedOutput)
        {
            // Arrange
            FirstSecondPersonInverter firstSecondPersonInverter = new FirstSecondPersonInverter();

            // Act
            string actualOutput = firstSecondPersonInverter.Convert(input);

            // Assert
            Assert.Equal(expectedOutput.ToLowerInvariant(), actualOutput.ToLowerInvariant());
        }

        [Theory]
        [InlineData("You are great.", "I am great.")]
        [InlineData("Steve says you are great.", "Steve says I am great.")]
        public void GivenSentenceWithYouAre_ShouldConvertToIam(string input, string expectedOutput)
        {
            // Arrange
            FirstSecondPersonInverter firstSecondPersonInverter = new FirstSecondPersonInverter();

            // Act
            string actualOutput = firstSecondPersonInverter.Convert(input);

            // Assert
            Assert.Equal(expectedOutput.ToLowerInvariant(), actualOutput.ToLowerInvariant());
        }

        [Theory]
        [InlineData("You're great.", "I'm great.")]
        [InlineData("Steve says you're great.", "Steve says I'm great.")]
        public void GivenSentenceWithYouRe_ShouldConvertToIm(string input, string expectedOutput)
        {
            // Arrange
            FirstSecondPersonInverter firstSecondPersonInverter = new FirstSecondPersonInverter();

            // Act
            string actualOutput = firstSecondPersonInverter.Convert(input);

            // Assert
            Assert.Equal(expectedOutput.ToLowerInvariant(), actualOutput.ToLowerInvariant());
        }

        [Theory]
        [InlineData("You were great.", "I was great.")]
        [InlineData("Steve says you were great.", "Steve says I was great.")]
        public void GivenSentenceWithYouWere_ShouldConvertToIWas(string input, string expectedOutput)
        {
            // Arrange
            FirstSecondPersonInverter firstSecondPersonInverter = new FirstSecondPersonInverter();

            // Act
            string actualOutput = firstSecondPersonInverter.Convert(input);

            // Assert
            Assert.Equal(expectedOutput.ToLowerInvariant(), actualOutput.ToLowerInvariant());
        }
        #endregion

        #region From Your to My
        [Theory]
        [InlineData("This is your car.", "This is my car.")]
        [InlineData("Your car is the best.", "My car is the best.")]
        public void GivenSentenceWithYour_ShouldConvertToMy(string input, string expectedOutput)
        {
            // Arrange
            FirstSecondPersonInverter firstSecondPersonInverter = new FirstSecondPersonInverter();

            // Act
            string actualOutput = firstSecondPersonInverter.Convert(input);

            // Assert
            Assert.Equal(expectedOutput.ToLowerInvariant(), actualOutput.ToLowerInvariant());
        }
        #endregion

        #region From Yours to Mine
        [Theory]
        [InlineData("This car is yours.", "This car is mine.")]
        public void GivenSentenceWithYours_ShouldConvertToMine(string input, string expectedOutput)
        {
            // Arrange
            FirstSecondPersonInverter firstSecondPersonInverter = new FirstSecondPersonInverter();

            // Act
            string actualOutput = firstSecondPersonInverter.Convert(input);

            // Assert
            Assert.Equal(expectedOutput.ToLowerInvariant(), actualOutput.ToLowerInvariant());
        }
        #endregion

        #region From Yourself to Myself
        [Theory]
        [InlineData("Steve told that to yourself.", "Steve told that to myself.")]
        public void GivenSentenceWithYourself_ShouldConvertToMyself(string input, string expectedOutput)
        {
            // Arrange
            FirstSecondPersonInverter firstSecondPersonInverter = new FirstSecondPersonInverter();

            // Act
            string actualOutput = firstSecondPersonInverter.Convert(input);

            // Assert
            Assert.Equal(expectedOutput.ToLowerInvariant(), actualOutput.ToLowerInvariant());
        }
        #endregion

        #region From Yourselves to Ourselves
        [Theory]
        [InlineData("Steve told that to yourselves.", "Steve told that to ourselves.")]
        public void GivenSentenceWithYourselves_ShouldConvertToOurselves(string input, string expectedOutput)
        {
            // Arrange
            FirstSecondPersonInverter firstSecondPersonInverter = new FirstSecondPersonInverter();

            // Act
            string actualOutput = firstSecondPersonInverter.Convert(input);

            // Assert
            Assert.Equal(expectedOutput.ToLowerInvariant(), actualOutput.ToLowerInvariant());
        }
        #endregion

        #region From Thou to I
        [Theory]
        [InlineData("Thou told that.", "I told that.")]
        public void GivenSentenceWithThou_ShouldConvertToI(string input, string expectedOutput)
        {
            // Arrange
            FirstSecondPersonInverter firstSecondPersonInverter = new FirstSecondPersonInverter();

            // Act
            string actualOutput = firstSecondPersonInverter.Convert(input);

            // Assert
            Assert.Equal(expectedOutput.ToLowerInvariant(), actualOutput.ToLowerInvariant());
        }
        #endregion

        #region From Thou to I
        [Theory]
        [InlineData("Steve told thee.", "Steve told me.")]
        public void GivenSentenceWithThee_ShouldConvertToMe(string input, string expectedOutput)
        {
            // Arrange
            FirstSecondPersonInverter firstSecondPersonInverter = new FirstSecondPersonInverter();

            // Act
            string actualOutput = firstSecondPersonInverter.Convert(input);

            // Assert
            Assert.Equal(expectedOutput.ToLowerInvariant(), actualOutput.ToLowerInvariant());
        }
        #endregion

        #region From Thy to My
        [Theory]
        [InlineData("Thy car is the best.", "My car is the best.")]
        public void GivenSentenceWithThy_ShouldConvertToMy(string input, string expectedOutput)
        {
            // Arrange
            FirstSecondPersonInverter firstSecondPersonInverter = new FirstSecondPersonInverter();

            // Act
            string actualOutput = firstSecondPersonInverter.Convert(input);

            // Assert
            Assert.Equal(expectedOutput.ToLowerInvariant(), actualOutput.ToLowerInvariant());
        }
        #endregion

        #region From Thyself to Myself
        [Theory]
        [InlineData("Steve told that to thyself.", "Steve told that to myself.")]
        public void GivenSentenceWithThyself_ShouldConvertToMyself(string input, string expectedOutput)
        {
            // Arrange
            FirstSecondPersonInverter firstSecondPersonInverter = new FirstSecondPersonInverter();

            // Act
            string actualOutput = firstSecondPersonInverter.Convert(input);

            // Assert
            Assert.Equal(expectedOutput.ToLowerInvariant(), actualOutput.ToLowerInvariant());
        }
        #endregion

        #region From Thine to Mine
        [Theory]
        [InlineData("This car is thine.", "This car is mine.")]
        public void GivenSentenceWithThine_ShouldConvertToMine(string input, string expectedOutput)
        {
            // Arrange
            FirstSecondPersonInverter firstSecondPersonInverter = new FirstSecondPersonInverter();

            // Act
            string actualOutput = firstSecondPersonInverter.Convert(input);

            // Assert
            Assert.Equal(expectedOutput.ToLowerInvariant(), actualOutput.ToLowerInvariant());
        }
        #endregion

        #region From Ye to I
        [Theory]
        [InlineData("Ye told that.", "I told that.")]
        public void GivenSentenceWithYe_ShouldConvertToI(string input, string expectedOutput)
        {
            // Arrange
            FirstSecondPersonInverter firstSecondPersonInverter = new FirstSecondPersonInverter();

            // Act
            string actualOutput = firstSecondPersonInverter.Convert(input);

            // Assert
            Assert.Equal(expectedOutput.ToLowerInvariant(), actualOutput.ToLowerInvariant());
        }
        #endregion

        #region From "you all", "y'all", "y'all's" and "you guys", "you people" to "us"
        [Theory]
        [InlineData("Steve told you all.", "Steve told us.")]
        [InlineData("Steve told you guys.", "Steve told us.")]
        [InlineData("Steve told you people.", "Steve told us.")]
        [InlineData("Steve told y'all.", "Steve told us.")]
        [InlineData("Steve told y'all's.", "Steve told us.")]
        public void GivenSentenceWithYouGuys_ShouldConvertToUs(string input, string expectedOutput)
        {
            // Arrange
            FirstSecondPersonInverter firstSecondPersonInverter = new FirstSecondPersonInverter();

            // Act
            string actualOutput = firstSecondPersonInverter.Convert(input);

            // Assert
            Assert.Equal(expectedOutput.ToLowerInvariant(), actualOutput.ToLowerInvariant());
        }
        #endregion
    }
}
