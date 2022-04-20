namespace Docomotion.Shared.EngineUtils
{
    public struct Measure_Units
    {
        public const string MM = "mm";
        public const string CM = "cm";
        public const string IN = "in";
        public const string PT = "pt";
        public const string PC = "pc";
        public const string PI = "pi";
    }


    public struct ST_OnOff
    {
        public const string ON_OFF_VAL_TRUE = "true";
        public const string ON_OFF_VAL_FALSE = "false";
        public const string ON_OFF_VAL_ON = "on";
        public const string ON_OFF_VAL_OFF = "off";
        public const string ON_OFF_VAL_0 = "0";
        public const string ON_OFF_VAL_1 = "1";
    }
    //-------------------------------------------------------------------

    public struct ST_HexColor
    {
        public const string COLOR_AUTO = "auto"; //Automatically Determined Color 
    }
    //-------------------------------------------------------------------

    public struct ST_Jc
    {
        public const string JC_LEFT = "left";
        public const string JC_CENTER = "center";
        public const string JC_RIGHT = "right";
        public const string JC_BOTH = "both";
        //~unsupported~enumeration value="mediumKashida";
        //~unsupported~enumeration value="distribute";
        //~unsupported~enumeration value="numTab";
        //~unsupported~enumeration value="highKashida";
        //~unsupported~enumeration value="lowKashida";
        //~unsupported~enumeration value="thaiDistribute";
    }
    //-------------------------------------------------------------------

    public struct ST_Underline
    {
        public const string UNDERLINE_SINGLE = "single";
        public const string UNDERLINE_NONE = "none";

        //~unsupported~enumeration value="words"/>
        //~unsupported~enumeration value="double"/>
        //~unsupported~enumeration value="thick"/>
        //~unsupported~enumeration value="dotted"/>
        //~unsupported~enumeration value="dottedHeavy"/>
        //~unsupported~enumeration value="dash"/>
        //~unsupported~enumeration value="dashedHeavy"/>
        //~unsupported~enumeration value="dashLong"/>
        //~unsupported~enumeration value="dashLongHeavy"/>
        //~unsupported~enumeration value="dotDash"/>
        //~unsupported~enumeration value="dashDotHeavy"/>
        //~unsupported~enumeration value="dotDotDash"/>
        //~unsupported~enumeration value="dashDotDotHeavy"/>
        //~unsupported~enumeration value="wave"/>
        //~unsupported~enumeration value="wavyHeavy"/>
        //~unsupported~enumeration value="wavyDouble"/>
    }
    //-------------------------------------------------------------------

    public struct ST_BrType
    {
        public const string BREAK_COLUMN = "column";
        public const string BREAK_PAGE = "page";
        public const string BREAK_TEXTWRAPPING = "textWrapping";
    }

    //-------------------------------------------------------------------

    public struct ST_BrClear
    {
        //~unsupported~public const string BREAK_CLEAR_NONE="none";
        //~unsupported~public const string BREAK_CLEAR_LEFT="left";
        //~unsupported~public const string BREAK_CLEAR_RIGHT="right";
        //~unsupported~public const string BREAK_CLEAR_ALL="all";
    }
    //-------------------------------------------------------------------
    public struct ST_TblWidth
    {
        public const string TYPE_NIL = "nil";
        public const string TYPE_PCT = "pct";
        public const string TYPE_DXA = "dxa";//Default
        public const string TYPE_AUTO = "auto";
    }
    //-------------------------------------------------------------------
    public struct ST_HeightRule
    {
        public const string RULE_AUTO = "auto";
        public const string RULE_EXACT = "exact";
        public const string RULE_AT_LEAST = "atLeast";
    }

    public struct ST_Border
    {
        public const string TYPE_NIL = "nil";
        public const string TYPE_NONE = "none";
        public const string TYPE_SINGLE = "single";

        public const string TYPE_DOTTED = "dotted";
        public const string TYPE_DASH_SMALL_GAP = "dashSmallGap";
        public const string TYPE_DASHED = "dashed";
        public const string TYPE_DOT_DOT_DASH = "dotDotDash";
        public const string TYPE_DOT_DASH = "dotDash";

        //~unsupported~<enumeration value="thick"/>
        //~unsupported~<enumeration value="double"/>
        //~unsupported~<enumeration value="triple"/>
        //~unsupported~<enumeration value="thinThickSmallGap"/>
        //~unsupported~<enumeration value="thickThinSmallGap"/>
        //~unsupported~<enumeration value="thinThickThinSmallGap"/>
        //~unsupported~<enumeration value="thinThickMediumGap"/>
        //~unsupported~<enumeration value="thickThinMediumGap"/>
        //~unsupported~<enumeration value="thinThickThinMediumGap"/>
        //~unsupported~<enumeration value="thinThickLargeGap"/>
        //~unsupported~<enumeration value="thickThinLargeGap"/>
        //~unsupported~<enumeration value="thinThickThinLargeGap"/>
        //~unsupported~<enumeration value="wave"/>
        //~unsupported~<enumeration value="doubleWave"/>
        //~unsupported~<enumeration value="dashDotStroked"/>
        //~unsupported~<enumeration value="threeDEmboss"/>
        //~unsupported~<enumeration value="threeDEngrave"/>
        //~unsupported~<enumeration value="outset"/>
        //~unsupported~<enumeration value="inset"/>
        //art borders
        //~unsupported~<enumeration value="apples"/>
        //~unsupported~<enumeration value="archedScallops"/>
        //~unsupported~<enumeration value="babyPacifier"/>
        //~unsupported~<enumeration value="babyRattle"/>
        //~unsupported~<enumeration value="balloons3Colors"/>
        //~unsupported~<enumeration value="balloonsHotAir"/>
        //~unsupported~<enumeration value="basicBlackDashes"/>
        //~unsupported~<enumeration value="basicBlackDots"/>
        //~unsupported~<enumeration value="basicBlackSquares"/>
        //~unsupported~<enumeration value="basicThinLines"/>
        //~unsupported~<enumeration value="basicWhiteDashes"/>
        //~unsupported~<enumeration value="basicWhiteDots"/>
        //~unsupported~<enumeration value="basicWhiteSquares"/>
        //~unsupported~<enumeration value="basicWideInline"/>
        //~unsupported~<enumeration value="basicWideMidline"/>
        //~unsupported~<enumeration value="basicWideOutline"/>
        //~unsupported~<enumeration value="bats"/>
        //~unsupported~<enumeration value="birds"/>
        //~unsupported~<enumeration value="birdsFlight"/>
        //~unsupported~<enumeration value="cabins"/>
        //~unsupported~<enumeration value="cakeSlice"/>
        //~unsupported~<enumeration value="candyCorn"/>
        //~unsupported~<enumeration value="celticKnotwork"/>
        //~unsupported~numeration value="certificateBanner"/1 >
        //~unsupported~<enumeration value="chainLink"/>
        //~unsupported~<enumeration value="champagneBottle"/>
        //~unsupported~<enumeration value="checkedBarBlack"/>
        //~unsupported~<enumeration value="checkedBarColor"/>
        //~unsupported~<enumeration value="checkered"/>
        //~unsupported~<enumeration value="christmasTree"/>
        //~unsupported~<enumeration value="circlesLines"/>
        //~unsupported~<enumeration value="circlesRectangles"/>
        //~unsupported~<enumeration value="classicalWave"/>
        //~unsupported~<enumeration value="clocks"/>
        //~unsupported~<enumeration value="compass"/>
        //~unsupported~<enumeration value="confetti"/>
        //~unsupported~<enumeration value="confettiGrays"/>
        //~unsupported~<enumeration value="confettiOutline"/>
        //~unsupported~<enumeration value="confettiStreamers"/>
        //~unsupported~<enumeration value="confettiWhite"/>
        //~unsupported~<enumeration value="cornerTriangles"/>
        //~unsupported~<enumeration value="couponCutoutDashes"/>
        //~unsupported~<enumeration value="couponCutoutDots"/>
        //~unsupported~<enumeration value="crazyMaze"/>
        //~unsupported~<enumeration value="creaturesButterfly"/>
        //~unsupported~<enumeration value="creaturesFish"/>
        //~unsupported~<enumeration value="creaturesInsects"/>
        //~unsupported~<enumeration value="creaturesLadyBug"/>
        //~unsupported~<enumeration value="crossStitch"/>
        //~unsupported~<enumeration value="cup"/>
        //~unsupported~<enumeration value="decoArch"/>
        //~unsupported~<enumeration value="decoArchColor"/>
        //~unsupported~<enumeration value="decoBlocks"/>
        //~unsupported~<enumeration value="diamondsGray"/>
        //~unsupported~<enumeration value="doubleD"/>
        //~unsupported~<enumeration value="doubleDiamonds"/>
        //~unsupported~<enumeration value="earth1"/>
        //~unsupported~<enumeration value="earth2"/>
        //~unsupported~<enumeration value="eclipsingSquares1"/>
        //~unsupported~<enumeration value="eclipsingSquares2"/>
        //~unsupported~<enumeration value="eggsBlack"/>
        //~unsupported~<enumeration value="fans"/>
        //~unsupported~<enumeration value="film"/>
        //~unsupported~<enumeration value="firecrackers"/>
        //~unsupported~<enumeration value="flowersBlockPrint"/>
        //~unsupported~<enumeration value="flowersDaisies"/>
        //~unsupported~<enumeration value="flowersModern1"/>
        //~unsupported~<enumeration value="flowersModern2"/>
        //~unsupported~<enumeration value="flowersPansy"/>
        //~unsupported~<enumeration value="flowersRedRose"/>
        //~unsupported~<enumeration value="flowersRoses"/>
        //~unsupported~<enumeration value="flowersTeacup"/>
        //~unsupported~<enumeration value="flowersTiny"/>
        //~unsupported~<enumeration value="gems"/>
        //~unsupported~<enumeration value="gingerbreadMan"/>
        //~unsupported~<enumeration value="gradient"/>
        //~unsupported~numeration value="1 handmade1"/>
        //~unsupported~<enumeration value="handmade2"/>
        //~unsupported~<enumeration value="heartBalloon"/>
        //~unsupported~<enumeration value="heartGray"/>
        //~unsupported~<enumeration value="hearts"/>
        //~unsupported~<enumeration value="heebieJeebies"/>
        //~unsupported~<enumeration value="holly"/>
        //~unsupported~<enumeration value="houseFunky"/>
        //~unsupported~<enumeration value="hypnotic"/>
        //~unsupported~<enumeration value="iceCreamCones"/>
        //~unsupported~<enumeration value="lightBulb"/>
        //~unsupported~<enumeration value="lightning1"/>
        //~unsupported~<enumeration value="lightning2"/>
        //~unsupported~<enumeration value="mapPins"/>
        //~unsupported~<enumeration value="mapleLeaf"/>
        //~unsupported~<enumeration value="mapleMuffins"/>
        //~unsupported~<enumeration value="marquee"/>
        //~unsupported~<enumeration value="marqueeToothed"/>
        //~unsupported~<enumeration value="moons"/>
        //~unsupported~<enumeration value="mosaic"/>
        //~unsupported~<enumeration value="musicNotes"/>
        //~unsupported~<enumeration value="northwest"/>
        //~unsupported~<enumeration value="ovals"/>
        //~unsupported~<enumeration value="packages"/>
        //~unsupported~<enumeration value="palmsBlack"/>
        //~unsupported~<enumeration value="palmsColor"/>
        //~unsupported~<enumeration value="paperClips"/>
        //~unsupported~<enumeration value="papyrus"/>
        //~unsupported~<enumeration value="partyFavor"/>
        //~unsupported~<enumeration value="partyGlass"/>
        //~unsupported~<enumeration value="pencils"/>
        //~unsupported~<enumeration value="people"/>
        //~unsupported~<enumeration value="peopleWaving"/>
        //~unsupported~<enumeration value="peopleHats"/>
        //~unsupported~<enumeration value="poinsettias"/>
        //~unsupported~<enumeration value="postageStamp"/>
        //~unsupported~<enumeration value="pumpkin1"/>
        //~unsupported~<enumeration value="pushPinNote2"/>
        //~unsupported~<enumeration value="pushPinNote1"/>
        //~unsupported~<enumeration value="pyramids"/>
        //~unsupported~<enumeration value="pyramidsAbove"/>
        //~unsupported~<enumeration value="quadrants"/>
        //~unsupported~<enumeration value="rings"/>
        //~unsupported~<enumeration value="safari"/>
        //~unsupported~<enumeration value="sawtooth"/>
        //~unsupported~<enumeration value="sawtoothGray"/>
        //~unsupported~<enumeration value="scaredCat"/>
        //~unsupported~<enumeration value="seattle"/>
        //~unsupported~<enumeration value="shadowedSquares"/>
        //~unsupported~<enumeration value="sharksTeeth"/>
        //~unsupported~<enumeration value="shorebirdTracks"/>
        //~unsupported~<enumeration value="skyrocket"/>
        //~unsupported~<enumeration value="snowflakeFancy"/>
        //~unsupported~<enumeration value="1 snowflakes"/>
        //~unsupported~<enumeration value="sombrero"/>
        //~unsupported~<enumeration value="southwest"/>
        //~unsupported~<enumeration value="stars"/>
        //~unsupported~<enumeration value="starsTop"/>
        //~unsupported~<enumeration value="stars3d"/>
        //~unsupported~<enumeration value="starsBlack"/>
        //~unsupported~<enumeration value="starsShadowed"/>
        //~unsupported~<enumeration value="sun"/>
        //~unsupported~<enumeration value="swirligig"/>
        //~unsupported~<enumeration value="tornPaper"/>
        //~unsupported~<enumeration value="tornPaperBlack"/>
        //~unsupported~<enumeration value="trees"/>
        //~unsupported~<enumeration value="triangleParty"/>
        //~unsupported~<enumeration value="triangles"/>
        //~unsupported~<enumeration value="tribal1"/>
        //~unsupported~<enumeration value="tribal2"/>
        //~unsupported~<enumeration value="tribal3"/>
        //~unsupported~<enumeration value="tribal4"/>
        //~unsupported~<enumeration value="tribal5"/>
        //~unsupported~<enumeration value="tribal6"/>
        //~unsupported~<enumeration value="twistedLines1"/>
        //~unsupported~<enumeration value="twistedLines2"/>
        //~unsupported~<enumeration value="vine"/>
        //~unsupported~<enumeration value="waveline"/>
        //~unsupported~<enumeration value="weavingAngles"/>
        //~unsupported~<enumeration value="weavingBraid"/>
        //~unsupported~<enumeration value="weavingRibbon"/>
        //~unsupported~<enumeration value="weavingStrips"/>
        //~unsupported~<enumeration value="whiteFlowers"/>
        //~unsupported~<enumeration value="woodwork"/>
        //~unsupported~<enumeration value="xIllusions"/>
        //~unsupported~<enumeration value="zanyTriangles"/>
        //~unsupported~<enumeration value="zigZag"/>
        //~unsupported~ <enumeration value="zigZagStitch"/>
    }

    public struct ST_Shd
    {
        //~unsupported~<enumeration value="nil"/>
        //~unsupported~<enumeration value="clear"/>
        //~unsupported~<enumeration value="solid"/>
        //~unsupported~<enumeration value="horzStripe"/>
        //~unsupported~<enumeration value="vertStripe"/>
        //~unsupported~<enumeration value="reverseDiagStripe"/>
        //~unsupported~<enumeration value="diagStripe"/>
        //~unsupported~<enumeration value="horzCross"/>
        //~unsupported~<enumeration value="diagCross"/>
        //~unsupported~<enumeration value="thinHorzStripe"/>
        //~unsupported~<enumeration value="thinVertStripe"/>
        //~unsupported~<enumeration value="thinReverseDiagStripe"/>
        //~unsupported~<enumeration value="thinDiagStripe"/>
        //~unsupported~<enumeration value="thinHorzCross"/>
        //~unsupported~<enumeration value="thinDiagCross"/>
        //~unsupported~<enumeration value="pct5"/>
        //~unsupported~<enumeration value="pct10"/>
        //~unsupported~<enumeration value="pct12"/>
        //~unsupported~<enumeration value="pct15"/>
        //~unsupported~<enumeration value="pct20"/>
        //~unsupported~<enumeration value="pct25"/>
        //~unsupported~<enumeration value="pct30"/>
        //~unsupported~<enumeration value="pct35"/>
        //~unsupported~<enumeration value="pct37"/>
        //~unsupported~<enumeration value="pct40"/>
        //~unsupported~<enumeration value="pct45"/>
        //~unsupported~<enumeration value="pct50"/>
        //~unsupported~<enumeration value="pct55"/>
        //~unsupported~<enumeration value="pct60"/>
        //~unsupported~<enumeration value="pct62"/>
        //~unsupported~<enumeration value="pct65"/>
        //~unsupported~<enumeration value="pct70"/>
        //~unsupported~<enumeration value="pct75"/>
        //~unsupported~<enumeration value="pct80"/>
        //~unsupported~<enumeration value="pct85"/>
        //~unsupported~<enumeration value="pct87"/>
        //~unsupported~<enumeration value="pct90"/>
        //~unsupported~<enumeration value="pct95"/>
    }

    public struct ST_VerticalJc
    {
        public const string VJC_TOP = "top";
        public const string VJC_CENTER = "center";
        public const string VJC_BOTTOM = "bottom";
        //~unsupported~<enumeration value="both"/>
    }

    public struct ST_TblLayoutType
    {
        public const string TBL_LAYOUT_TYPE_FIXED = "fixed";
        public const string TBL_LAYOUT_TYPE_AUTOFIT = "autofit";
    }

    public struct ST_NumberFormat
    {
        public const string NUMBERING_FORMAT_DECIMAL = "decimal";//<enumeration value="decimal"/>
        public const string NUMBERING_FORMAT_UPPER_ROMAN = "upperRoman"; //<enumeration value="upperRoman"/>
        public const string NUMBERING_FORMAT_LOWER_ROMAN = "lowerRoman"; //<enumeration value="lowerRoman"/>
        public const string NUMBERING_FORMAT_UPPERLETTER = "upperLetter"; //<enumeration value="upperLetter"/>
        public const string NUMBERING_FORMAT_LOWER_LETTER = "lowerLetter"; //<enumeration value="lowerLetter"/>
        //~unsupported~<enumeration value="ordinal"/>
        //~unsupported~<enumeration value="cardinalText"/>
        //~unsupported~<enumeration value="ordinalText"/>
        //~unsupported~<enumeration value="hex"/>
        public const string NUMBERING_FORMAT_CHICAGO = "chicago"; //<enumeration value="chicago"/>
        //~unsupported~<enumeration value="ideographDigital"/>
        //~unsupported~<enumeration value="japaneseCounting"/>
        //~unsupported~<enumeration value="aiueo"/>
        //~unsupported~<enumeration value="iroha"/>
        //~unsupported~<enumeration value="decimalFullWidth"/>
        //~unsupported~<enumeration value="decimalHalfWidth"/>
        //~unsupported~<enumeration value="japaneseLegal"/>
        //~unsupported~<enumeration value="japaneseDigitalTenThousand"/>
        //~unsupported~<enumeration value="decimalEnclosedCircle"/>
        //~unsupported~<enumeration value="decimalFullWidth2"/>
        //~unsupported~<enumeration value="aiueoFullWidth"/>
        //~unsupported~<enumeration value="irohaFullWidth"/>
        public const string NUMBERING_FORMAT_DECIMALZERO = "decimalZero";//<enumeration value="decimalZero"/>
        public const string NUMBERING_FORMAT_BULLET = "bullet";//<enumeration value="bullet"/>
        //~unsupported~<enumeration value="ganada"/>
        //~unsupported~<enumeration value="chosung"/>
        //~unsupported~<enumeration value="decimalEnclosedFullstop"/>
        //~unsupported~<enumeration value="decimalEnclosedParen"/>
        //~unsupported~<enumeration value="decimalEnclosedCircleChinese"/>
        //~unsupported~<enumeration value="ideographEnclosedCircle"/>
        //~unsupported~<enumeration value="ideographTraditional"/>
        //~unsupported~<enumeration value="ideographZodiac"/>
        //~unsupported~<enumeration value="ideographZodiacTraditional"/>
        //~unsupported~<enumeration value="taiwaneseCounting"/>
        //~unsupported~<enumeration value="ideographLegalTraditional"/>
        //~unsupported~<enumeration value="taiwaneseCountingThousand"/>
        //~unsupported~<enumeration value="taiwaneseDigital"/>
        //~unsupported~<enumeration value="chineseCounting"/>
        //~unsupported~<enumeration value="chineseLegalSimplified"/>
        //~unsupported~<enumeration value="chineseCountingThousand"/>
        //~unsupported~<enumeration value="koreanDigital"/>
        //~unsupported~<enumeration value="koreanCounting"/>
        //~unsupported~<enumeration value="koreanLegal"/>
        //~unsupported~<enumeration value="koreanDigital2"/>
        //~unsupported~<enumeration value="vietnameseCounting"/>
        //~unsupported~<enumeration value="russianLower"/>
        //~unsupported~<enumeration value="russianUpper"/>
        //~unsupported~<enumeration value="none"/>
        public const string NUMBERING_FORMAT_NUMBER_IN_DASH = "numberInDash";//~unsupported~<enumeration value="numberInDash"/>
        public const string NUMBERING_FORMAT_HEBREW1 = "hebrew1";//<enumeration value="hebrew1"/>
        public const string NUMBERING_FORMAT_HEBREW2 = "hebrew2"; //<enumeration value="hebrew2"/>
        public const string NUMBERING_FORMAT_ARABICDASH = "ArabicDash"; //<enumeration value="ArabicDash"/>
        //~unsupported~public const string NUMBERING_FORMAT_ARABICALPHA = "ArabicAlpha";//~unsupported~<enumeration value="arabicAlpha"/>
        //~unsupported~public const string NUMBERING_FORMAT_ARABICABJAD = "ArabicAbjad";//~unsupported~<enumeration value="arabicAbjad"/>
        //~unsupported~<enumeration value="hindiVowels"/>
        //~unsupported~<enumeration value="hindiConsonants"/>
        //~unsupported~<enumeration value="hindiNumbers"/>
        //~unsupported~<enumeration value="hindiCounting"/>
        //~unsupported~<enumeration value="thaiLetters"/>
        //~unsupported~<enumeration value="thaiNumbers"/>
        //~unsupported~<enumeration value="thaiCounting"/>
    }

    public struct WMLGeneralFormattingSwitchArguments
    {
        public const string AIUEO = "AIUEO";//Corresponds to an ST_NumberFormat enumeration value of aiueoFullWidth.;
        public const string ALPHABETIC = "ALPHABETIC";//Corresponds to an ST_NumberFormat value of upperLetter.
        public const string Alphabetic = "alphabetic";//Corresponds to an ST_NumberFormat value of lowerLetter.
        public const string TEXT_BOTTOM = "text-bottom";//Corresponds to an ST_NumberFormat enumeration value of lowerLetter.
        public const string Arabic = "Arabic";//Corresponds to an ST_NumberFormat enumeration value of decimal.
        public const string ARABICABJAD = "ARABICABJAD";//Corresponds to an ST_NumberFormat enumeration value of arabicAbjad.
        public const string ARABICALPHA = "ARABICALPHA";//Corresponds to an ST_NumberFormat enumeration value of arabicAlpha.
        public const string ArabicDash = "ArabicDash";//Corresponds to an ST_NumberFormat enumeration value of numberInDash.
        public const string BAHTTEXT = "BAHTTEXT";//Corresponds to an ST_NumberFormat enumeration value of bahtText.
        public const string CardText = "CardText";//Corresponds to an ST_NumberFormat enumeration value of cardinalText.
        public const string CHINESENUM1 = "CHINESENUM1";//Corresponds to an ST_NumberFormat enumeration value of chineseCounting(zh-CN) or taiwaneseCounting (zn-TW).
        public const string CHINESENUM2 = "CHINESENUM2";//Corresponds to an ST_NumberFormat enumeration value of chineseLegalSimplified (zh-CN) or ideographLegalTraditional (zh-TW).
        public const string CHINESENUM3 = "CHINESENUM3";//Corresponds to an ST_NumberFormat enumeration value of chineseCountingThousand (zh-CN) or taiwaneseCountingThousand (zh-TW).
        public const string CHOSUNG = "CHOSUNG";//Corresponds to an ST_NumberFormat enumeration value of chosung.
        public const string CIRCLENUM = "CIRCLENUM";//Corresponds to an ST_NumberFormat enumeration value of decimalEnclosedCircle.
        public const string DBCHAR = "DBCHAR";//Corresponds to an ST_NumberFormat enumeration value of decimalFullWidth.
        public const string DBNUM1 = "DBNUM1";//Corresponds to an ST_NumberFormat enumeration value of ideographDigital (ja-JP) or koreanDigital (ko-KR).
        public const string DBNUM2 = "DBNUM2";//Corresponds to an ST_NumberFormat enumeration value of japaneseCounting (ja-JP) or koreanCounting (ko-KR).
        public const string DBNUM3 = "DBNUM3";//Corresponds to an ST_NumberFormat enumeration value of japaneseLegal (ja-JP) or koreanLegal (ko-KR).
        public const string DBNUM4 = "DBNUM4";//Corresponds to an ST_NumberFormat enumeration value of japaneseDigitalTenThousand (ja-JP) or koreanDigital2 (ko-KR) or taiwaneseDigital (zh-TW).
        public const string DollarText = "DollarText";//Corresponds to an ST_NumberFormat enumeration value of dollarText.
        public const string GANADA = "GANADA";//Corresponds to an ST_NumberFormat enumeration value of ganada.
        public const string GB1 = "GB1";//Corresponds to an ST_NumberFormat enumeration value of decimalEnclosedFullstop.
        public const string GB2 = "GB2";//Corresponds to an ST_NumberFormat enumeration value of decimalEnclosedParen.
        public const string GB3 = "GB3";//Corresponds to an ST_NumberFormat enumeration value of decimalEnclosedCircleChinese.
        public const string GB4 = "GB4";//Corresponds to an ST_NumberFormat enumeration value of ideographEnclosedCircle.
        public const string HEBREW1 = "HEBREW1";//Corresponds to an ST_NumberFormat enumeration value of hebrew1.
        public const string HEBREW2 = "HEBREW2";//Corresponds to an ST_NumberFormat enumeration value of hebrew2.
        public const string Hex = "Hex";//Corresponds to an ST_NumberFormat enumeration value of hex.
        public const string HINDIARABIC = "HINDIARABIC";//Corresponds to an ST_NumberFormat enumeration value of hindiNumbers.
        public const string HINDICARDTEXT = "HINDICARDTEXT";//Corresponds to an ST_NumberFormat enumeration value of hindiCounting.
        public const string HINDILETTER1 = "HINDILETTER1";//Corresponds to an ST_NumberFormat enumeration value of hindiVowels.
        public const string HINDILETTER2 = "HINDILETTER2";//Corresponds to an ST_NumberFormat enumeration value of hindiConsonants.
        public const string IROHA = "IROHA";//Corresponds to an ST_NumberFormat enumeration value of irohaFullWidth.
        public const string KANJINUM1 = "KANJINUM1";//Corresponds to an ST_NumberFormat enumeration value of koreanDigital (ko-KR), ideographDigital (ja-JP), chineseCounting (zh-CN), or taiwaneseCounting (zh-TW).
        public const string KANJINUM2 = "KANJINUM2";//Corresponds to an ST_NumberFormat enumeration value of koreanCounting (ko-KR), chineseCountingThousand (ja-JP), chineseLegalSimplified (zh-CN),or ideographLegalTraditional (zh-TW).
        public const string KANJINUM3 = "KANJINUM3";//Corresponds to an ST_NumberFormat enumeration value of koreanLegal (ko-KR) or japaneseLegal (ja-JP) or chineseCountingThousand (zh-CN) ortaiwaneseCountingThousand (zh-TW).
        public const string Ordinal = "Ordinal";//Corresponds to an ST_NumberFormat enumeration value of ordinal.
        public const string OrdText = "OrdText";//Corresponds to an ST_NumberFormat enumeration value of ordinalText.
        public const string ROMAN = "ROMAN";//Corresponds to an ST_NumberFormat enumeration value of upperRoman.
        public const string roman = "roman";//Corresponds to an ST_NumberFormat enumeration value of lowerRoman.
        public const string SBCHAR = "SBCHAR";//Corresponds to an ST_NumberFormat enumeration value of decimalHalfWidth.
        public const string THAIARABIC = "THAIARABIC";//Corresponds to an ST_NumberFormat enumeration value of thaiNumbers.
        public const string THAICARDTEXT = "THAICARDTEXT";//Corresponds to an ST_NumberFormat enumeration value of thaiCounting.
        public const string THAILETTER = "THAILETTER";//Corresponds to an ST_NumberFormat enumeration value of thaiLetters.
        public const string VIETCARDTEXT = "VIETCARDTEXT";//Corresponds to an ST_NumberFormat enumeration value of vietnameseCounting.
        public const string ZODIAC1 = "ZODIAC1";//Corresponds to an ST_NumberFormat enumeration value of ideographTraditional.
        public const string ZODIAC2 = "ZODIAC2";//Corresponds to an ST_NumberFormat enumeration value of ideographZodiac.
        public const string ZODIAC3 = "ZODIAC3";//Corresponds to an ST_NumberFormat enumeration value of ideographZodiacTraditional
    }

    public struct Feild_Definitions
    {
        public const string TIME = "TIME";
        public const string DATE = "DATE";
        public const string NUMPAGES = "NUMPAGES";
        public const string PAGE = "PAGE";
        public const string AUTONUM = "AUTONUM";
        public const string HYPERLINK = "HYPERLINK";
        public const string SECTIONPAGES = "SECTIONPAGES";

        public const string PAGEREF = "PAGEREF";
        public const string TOC = "TOC";
    }

    public struct ST_FldCharType
    {
        public const string CHAR_TYPE_BEGIN = "begin";
        public const string CHAR_TYPE_SEPARATE = "separate";
        public const string CHAR_TYPE_END = "end";
    }

    public struct ST_VerticalAlignRun
    {
        public const string VALIGN_RUN_BASELINE = "baseline";
        public const string VALIGN_RUN_SUPERSCRIPT = "superscript";
        public const string VALIGN_RUN_SUBSCRIPT = "subscript";
    }

    public struct ST_FtnEdn
    {
        public const string FOOT_END_NOTE_SEPARATOR = "separator";
        public const string FOOT_END_NOTE_CONTINUATION_SEPARATOR = "continuationSeparator";
    }

    public struct ST_TabJc
    {
        public const string CLEAR = "clear";
        public const string LEFT = "left";
        public const string CENTER = "center";
        public const string RIGHT = "right";
        //~unsupported~<enumeration value="decimal"/>
        //~unsupported~<enumeration value="bar"/>
        //~unsupported~<enumeration value="num"/>
    }
    public struct ST_TabTlc
    {
        public const string NONE = "none";
        public const string DOT = "dot";
        public const string HYPHEN = "hyphen";
        public const string UNDERSCORE = "underscore";
        //~unsupported~<xsd:enumeration value="heavy"/>
        //~unsupported~<xsd:enumeration value="middleDot"/>
    }

    public struct MSO_PpositionHorizontal
    {
        public const string ABSOLUTE = "absolute";
        public const string LEFT = "left";
        public const string CENTER = "center";
        public const string RIGHT = "right";
        public const string INSIDE = "inside";
        public const string OUTSIDE = "outside";
    }
    public struct MSO_PositionHorizontalRelative
    {
        public const string MARGIN = "margin";
        public const string PAGE = "page";
        public const string TEXT = "text";
        public const string CHAR = "char";
    }
    public struct MSO_PositionVertical
    {
        public const string ABSOLUTE = "absolute";
        public const string TOP = "top";
        public const string CENTER = "center";
        public const string BOTTOM = "bottom";
        public const string INSIDE = "inside";
        public const string OUTSIDE = "outside";
    }

    public struct MSO_WrapStyle
    {
        public const string SQUARE = "square";
        public const string NONE = "none";
    }

    public struct MSO_PositionVerticalRelative
    {
        public const string MARGIN = "margin";
        public const string PAGE = "page";
        public const string TEXT = "text";
        public const string LINE = "line";
    }

    public struct Shape_Layout_Flow
    {
        public const string HORIZONTAL = "horizontal";
        public const string VERTICAL = "vertical";
        public const string VERTICAL_IDEOGRAPHIC = "vertical-ideographic";
        public const string HORIZONTAL_IDEOGRAPHIC = "horizontal_ideographic";
    }

    public struct Shape_Position
    {
        public const string STATIC = "static";
        public const string ABSOLUTE = "absolute";
        public const string RELATIVE = "relative";
    }

    public struct ST_LineSpacingRule
    {
        public const string ATLEAST = "atLeast";
        public const string AUTO = "auto";
        public const string EXACT = "exact";
    }

    public struct ST_WRAP_TYPE // should change in VMLShape
    {
        public const string IN_LINE_WITH_TEXT = "none";
        public const string SQUARE = "square";
        public const string TIGHT = "tight";
        public const string THROUGH = "through";
        public const string TOP_AND_BOTTOM = "topAndBottom";
        public const string IN_FRONT_OF_TEXT = "InFrontOfText";
        public const string BEHIND_TEXT = "BehindText";
    }

    public struct ST_RelFromH
    {
        public const string MARGIN = "margin";
        public const string PAGE = "page";
        public const string COLUMN = "column";
        public const string CHARACTER = "character";
        public const string LEFT_MARGIN = "leftMargin";
        public const string RIGHT_MARGIN = "rightMargin";
        public const string INSIDE_MARGIN = "insideMargin";
        public const string OUTSIDE_MARGIN = "outsideMargin";
    }
    public struct ST_RelFromV
    {
        public const string MARGIN = "margin";
        public const string PAGE = "page";
        public const string PARAGRAPH = "paragraph";
        public const string LINE = "line";
        public const string TOP_MARGIN = "topMargin";
        public const string BOTTOM_MARGIN = "bottomMargin";
        public const string INSIDE_MARGIN = "insideMargin";
        public const string OUTSIDE_MARGIN = "outsideMargin";
    }

    public struct ST_Merge
    {
        public const string CONTINUE = "continue";
        public const string RESTART = "restart";
    }

    public struct ST_Theme
    {
        public const string MAJOR_ASCII = "majorAscii";
        public const string MAJOR_BIDI = "majorBidi";
        public const string MAJOR_EAST_ASIA = "majorEastAsia";
        public const string MAJOR_HANSI = "majorHAnsi";
        public const string MINOR_ASCII = "minorAscii";
        public const string MINOR_BIDI = "minorBidi";
        public const string MINOR_EAST_ASIA = "minorEastAsia";
        public const string MINOR_HANSI = "minorHAnsi";
    }

    public struct ST_Lang //§2.18.51 (Language Reference)
    {
        public const string ENG_ISO = "en-US";
        public const string ENG_CODE = "1033";
        public const string HEB_ISO = "he-IL";
        public const string HEB_CODE = "1037";
        //~unsupported~all other language in ISO 639-1 & ISO 3166-1 alpha-2
        //~unsupported~all other language in ST_LangCode §2.18.52
    }

    public struct ST_LevelSuffix //2.18.53(Content Between Numbering Symbol and Paragraph Text)
    {
        public const string NOTHING = "nothing";
        public const string TAB = "tab";
        public const string SPACE = "space"; //<enumeration value="space"/>
    }


    public struct ST_StyleType //2.18.90 ST_StyleType (Style Types)
    {
        public const string PARAGRAPH = "paragraph";
        public const string CHARACTER = "character";
        public const string TABLE = "table";
        public const string NUMBERING = "numbering";
    }


    public struct ST_VAnchor //2.18.109 ST_VAnchor (Vertical Anchor Location)
    {
        public const string TEXT = "text";
        public const string MARGIN = "margin";
        public const string PAGE = "page";
    }
    public struct ST_HAnchor //2.18.40 ST_HAnchor (Horizontal Anchor Location)
    {
        public const string TEXT = "text";
        public const string MARGIN = "margin";
        public const string PAGE = "page";
    }

    public struct ST_YAlign
    {
        public const string INLINE = "inline";
        public const string TOP = "top";
        public const string CENTER = "center";
        public const string BOTTOM = "bottom";
        public const string INSIDE = "inside";
        public const string OUTSIDE = "outside";
    }

    public struct ST_XAlign
    {
        public const string LEFT = "left";
        public const string CENTER = "center";
        public const string RIGHT = "right";
        public const string INSIDE = "inside";
        public const string OUTSIDE = "outside";
    }

    public struct ST_SectionMark//<simpleType name="ST_SectionMark">
    {
        public const string NEXT_PAGE = "nextPage";
        public const string NEXT_COLUMN = "nextColumn";
        public const string CONTINUOUS = "continuous";
        public const string EVEN_PAGE = "evenPage";
        public const string ODD_PAGE = "oddPage";
    }

    public struct ST_TblStyleOverrideType
    {
        public const string BAND1_HORZ = "band1Horz"; //(Banded Row Conditional Formatting) Specifies that the table formatting applies to odd numbered groupings of rows.
        public const string BAND1_VERT = "band1Vert"; //(Banded Column Conditional Formatting) Specifies that the table formatting applies to odd numbered groupings of columns.
        public const string BAND2_HORZ = "band2Horz"; //(Even Row Stripe Conditional Formatting) Specifies that the table formatting applies to even numbered groupings of rows.
        public const string BAND2_VERT = "band2Vert"; //(Even Column Stripe ConditionalFormatting) Specifies that the table formatting applies to even numbered groupings of columns.
        public const string FIRST_COL = "firstCol"; //(First Column Conditional Formatting) Specifies that the table formatting applies to the first column.
        public const string FIRST_ROW = "firstRow"; //(First Row Conditional Formatting) Specifies that the table formatting applies to the first row. Any subsequent row which has the tblHeader element present (§2.4.46) shall also use this conditional format.
        public const string LAST_COL = "lastCol"; //(Last table column formatting) Specifies that the table formatting applies to the last column.
        public const string LAST_ROW = "lastRow"; //(Last table row formatting) Specifies that the table formatting applies to the last row.
        public const string TOP_RIGHT_CELL = "neCell"; //(Top right table cell formatting) Specifies that the table formatting applies to the top right cell.
        public const string TOP_LEFT_CELL = "nwCell"; //(Top left table cell formatting) Specifies that the table formatting applies to the top left cell.
        public const string BOTTOM_RIGHT_CELL = "seCell"; //(Bottom right table cell formatting) Specifies that the table formatting applies to the bottom right cell.
        public const string BOTTOM_LEFT_CELL = "swCell"; //(Bottom left table cell formatting) Specifies that the table formatting applies to the bottom left cell.
        public const string WHOLE_TABLE = "wholeTable"; //(Whole table formatting)
    }

    public struct ST_PageOrientation
    {
        public const string PORTRAIT = "portrait";
        public const string LANDSCAPE = "landscape";
    }

    public struct ST_TargetMode
    {
        public const string EXTERNAL = "External";
        public const string INTERNAL = "Internal";
    }

    public struct ST_ShapeType
    {
        public const string LINE = "line";
        public const string RECT = "rect";
        public const string ROUND_RECT = "roundRect";
        public const string ELLIPSE = "ellipse";
    }

    public struct ST_TextWrappingType
    {
        public const string NONE = "none";
        public const string SQUARE = "square";
    }

    public struct ST_SchemeColorVal
    {
        public const string BG1 = "bg1";
        public const string TX1 = "tx1";
        public const string BG2 = "bg2";
        public const string TX2 = "tx2";
        public const string ACCENT1 = "accent1";
        public const string ACCENT2 = "accent2";
        public const string ACCENT3 = "accent3";
        public const string ACCENT4 = "accent4";
        public const string ACCENT5 = "accent5";
        public const string ACCENT6 = "accent6";
        public const string HLINK = "hlink";
        public const string FOLHLINK = "folHlink";
        public const string PHCLR = "phClr";
        public const string DK1 = "dk1";
        public const string LT1 = "lt1";
        public const string DK2 = "dk2";
        public const string LT2 = "lt2";
    }

    public struct ST_TextAnchoringType
    {
        public const string TOP = "t";
        public const string CENTER = "ctr";
        public const string BOTTOM = "b";
        //~unsupported~public const string value="just";
        //~unsupported~public const string value = "dist";
    }

    public struct ST_WmlColorSchemeIndex
    {
        public const string DARK1 = "dark1";
        public const string LIGHT1 = "light1";
        public const string DARK2 = "dark2";
        public const string LIGHT2 = "light2";
        public const string ACCENT1 = "accent1";
        public const string ACCENT2 = "accent2";
        public const string ACCENT3 = "accent3";
        public const string ACCENT4 = "accent4";
        public const string ACCENT5 = "accent5";
        public const string ACCENT6 = "accent6";
        public const string HYPERLINK = "hyperlink";
        public const string FOLLOWEDHYPERLINK = "followedHyperlink";
    }

    public struct ST_PresetLineDashVal
    {
        public const string SOLID = "solid";
        public const string DOT = "dot";
        public const string DASH = "dash";
        public const string LGDASH = "lgDash";
        public const string DASHDOT = "dashDot";
        public const string LGDASHDOT = "lgDashDot";
        public const string LGDASHDOTDOT = "lgDashDotDot";
        public const string SYSDASH = "sysDash";
        public const string SYSDOT = "sysDot";
        public const string SYSDASHDOT = "sysDashDot";
        public const string SYSDASHDOTDOT = "sysDashDotDot";
    }

    public struct ST_TextVerticalType
    {
        public const string HORZ = "horz";
        public const string VERT = "vert";
        public const string VERT270 = "vert270";

        // <xsd:enumeration value="wordArtVert"/>
        // <xsd:enumeration value="eaVert"/>
        // <xsd:enumeration value="mongolianVert"/>
        // <xsd:enumeration value="wordArtVertRtl"/>
    }

    public struct ST_Hint
    {
        public const string DEFAULT = "default";
        public const string EASTASIA = "eastAsia";
        public const string CS = "cs";
    }

    public struct ST_DisplacedByCustomXml
    {
        public const string NEXT = "next";
        public const string PREV = "prev";
    }

    public struct ST_HyperLinkTargets
    {
        public const string BLANK = "_blank";// Opens the linked document in a new window or tab
        public const string SELF = "_self";//   Opens the linked document in the same frame as it was clicked(this is default)
        public const string PARENT = "_parent";// Opens the linked document in the parent frame
        public const string TOP = "_top";//    Opens the linked document in the full body of the window
    }

    public struct ST_Lock
    {
        public const string CONTENTLOCKED = "contentLocked"; //Contents Cannot Be Edited At Runtime
        public const string UNLOCKED = "unlocked";//No Locking
        public const string SDTLOCKED = "sdtLocked";//SDT Cannot Be Deleted
        public const string SDTCONTENTLOCKED = "sdtContentLocked";//Contents Cannot Be Edited At Runtime And SDT Cannot Be Deleted
    }
}
