namespace EnumNamespace
{
    //adjectives/properties that knowables can have e.g. tasty. gray.
    public enum Adjective { none, good, bad, nice, tasty, healthy, ripe, strong, hot }
    //public enum Verb { none, taste, smell, look, help }
    //only includes spatial prepositions
    public enum Preposition { none, at, on, @in, under, over, inside, beside, above, below }

    public enum Tense { none, pastSimple, present, presentContinuous, future }
    //public enum temporalRelationships {before, after }

    public enum typeOfStuff { none, country, town, supermarket, bakery, table, bread, cake }
    public enum typeofPlace { none, world, country, state, city, town, village, house, shop, supermarket, farmland, forest }

    public enum AccessLevel
    {
        BLACKLIST,//this is the default value, knowledge which is known by all (has blacklist of exceptions). friending will not override
        PUBLIC, //aquaintance. for knowledge known by some. by default agents know public and obvious info like that a house has a red front door, but not what is inside
        PRIVATE, //friend. by default even its existence is unknown. friending gives access to public info and also public info of all children until a certain depth. e.g. everything inside a house, but not what is in the drawers and cupboards
        WHITELIST //for secrets known only by a select few. friending will not ovveride black or white list
    };
}