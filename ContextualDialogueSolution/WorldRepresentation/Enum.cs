namespace EnumNamespace
{
    //adjectives/properties that knowables can have e.g. tasty. gray.
    public enum adjective { none, good, bad, nice, tasty, healthy, ripe, strong, hot}
    public enum verb { none, taste, smell, look }
    //only includes spatial prepositions
    public enum Preposition { none, at, on, @in, under, over, inside, beside, above, below }
    public enum Tense { none, pastSimple, present, future}
    //public enum temporalRelationships {before, after }

    public enum typeOfStuff { none, country, town, supermarket, bakery, table, bread, cake}
    public enum typeofPlace { none, world, country, state, city, town, village, house, shop, supermarket, farmland, forest}
}