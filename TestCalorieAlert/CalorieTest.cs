using PROG6221_POE;

namespace TestCalorieAlert
{
    [TestClass]
    public class TestRecipeClass
    {
        // Create instances of Recipe class
        Recipe recipeClass = new PROG6221_POE.Recipe();
        Recipe recipeLow = new Recipe();
        Recipe recipeMedium = new Recipe();
        Recipe recipeMediumHigh = new Recipe();
        Recipe recipeHigh = new Recipe();

        // Create instances of Ingredient class
        Ingredient ingredientLow = new Ingredient("Eggs Whites", "Cup(s)", 0.3, 30, "Protein");
        Ingredient ingredientMedium = new Ingredient("Egg", "Cup(s)", 0.3, 100, "Protein");
        Ingredient ingredientMediumHigh = new Ingredient("Bacon", "Gram(s)", 200, 200, "Fats Or Oils");
        Ingredient ingredientHigh = new Ingredient("Butter", "Cup(s)", 1, 1672, "Fats Or Oils");

        private int recipeScale = 1;

        // Constructor
        public TestRecipeClass()
        {
            // Add ingredients to the corresponding recipe
            recipeLow.IngredientsList.Add(ingredientLow);
            recipeMedium.IngredientsList.Add(ingredientLow);
            recipeMedium.IngredientsList.Add(ingredientMedium);
            recipeMediumHigh.IngredientsList.Add(ingredientMediumHigh);
            recipeHigh.IngredientsList.Add(ingredientHigh);
        }

        //----------------------------------------------------------------------------\\

        // Test method for low-calorie recipe
        [TestMethod]
        public void TestLowCalorieInfo()
        {
            // Calculate the total calories for the recipe with the given scale
            var result = recipeClass.GetCalorieInformation(recipeLow.TotalCalories(recipeScale));
            var expected = "[30 calories is considered very low in energy content," +
                " providing minimal nutrients for the body]";

            // Assert that the calculated calorie information matches the expected result
            Assert.AreEqual(expected, result);
        }

        //----------------------------------------------------------------------------\\

        // Test method for medium-calorie recipe
        [TestMethod]
        public void TestMediumCalorieInfo()
        {
            // Calculate the total calories for the recipe with the given scale
            var result = recipeClass.GetCalorieInformation(recipeMedium.TotalCalories(recipeScale));
            var expected = "[130 calories, is a relatively low amount of calories," +
                " offering only a modest amount of nutrients]";

            // Assert that the calculated calorie information matches the expected result
            Assert.AreEqual(expected, result);
        }

        //----------------------------------------------------------------------------\\

        // Test method for medium-high-calorie recipe
        [TestMethod]
        public void TestMediumHighCalorieInfo()
        {
            // Calculate the total calories for the recipe with the given scale
            var result = recipeClass.GetCalorieInformation(recipeMediumHigh.TotalCalories(recipeScale));
            var expected = "[200 calories provides a slightly more substantial energy " +
                "content, offering a bit more nutrients for the body]";

            // Assert that the calculated calorie information matches the expected result
            Assert.AreEqual(expected, result);
        }

        //----------------------------------------------------------------------------\\

        // Test method for high-calorie recipe
        [TestMethod]
        public void TestHighCalorieInfo()
        {
            // Calculate the total calories for the recipe with the given scale
            var result = recipeClass.GetCalorieInformation(recipeHigh.TotalCalories(recipeScale));
            var expected = "[1672 calories is considered significant in terms of energy " +
                "content, providing a very substantial amount of nutrients for the body]";

            // Assert that the calculated calorie information matches the expected result
            Assert.AreEqual(expected, result);
        }
    }
}
