public static class GameConfig
{
    public const string GAME_NAME = "FINGER VS BUGS";
    public const string GAME_PACKAGE_NAME = "com.gigadrillgames.fingervsants";
    public const string GAME_VERSION = "V1.17";

    // game settings
    public const string GAME_TITLE = "Finger VS Bugs";
    public const string LOCAL_NOTIFICATION_MESSAGE = "We are ready for battle!";
    public const string LOCAL_NOTIFICATION_MESSAGE2 = "Come and fight us!";
    public const string LOCAL_NOTIFICATION_MESSAGE3 = "We are waiting for you!";
    public const string LOCAL_NOTIFICATION_MESSAGE4 = "Let's fight now!";
    public const string LOCAL_NOTIFICATION_TICKER_MESSAGE = "Hey are you there?";

    public const float MUSIC_DEFAULT_VOLUME = 0.5f;
    public const float SOUND_EFFECTS_DEFAULT_VOLUME = 0.5f;

    public const int VIBRATION_ON = 1;
    public const int VIBRATION_OFF = 2;

    public const float GAME_OVER_CLICK_DELAY = 0.70f;
    public const float REWARD_VIDEO_AD_CLICK_DELAY = 0.70f;
    // game settings

    // google ads
    public const string ADMOB_TEST_AD_UNIT_ID = "E194DA0657465C81B4AE7B90B4B32365";
    public const string ADMOB_BANNER_AD_UNIT_ID = "ca-app-pub-8754873203122588/6141746158";
    public const string ADMOB_INTERTITIAL_AD_UNIT_ID = "ca-app-pub-8754873203122588/4030727759";
    public const string ADMOB_REWARDED_INTERTITIAL_AD_UNIT_ID = "ca-app-pub-8754873203122588/6560958956";
    public const int GAME_COUNT_CLASSIC_INTERSTITIAL = 3;
    public const int GAME_COUNT_KIDS_INTERSTITIAL = 5;
    public const bool IS_ADMOB_TEST_ADS = false;
    public const bool HAS_REWARDED_BASED_VIDEO_ADS = true;
    //google ad

	
    //game action
    public const int ACTION_NONE = -1;
    public const int ACTION_SIGN_IN = 0;
    public const int ACTION_SIGN_OUT = 1;
    public const int ACTION_SHOW_LEADER_BOARD = 2;
    public const int ACTION_SHOW_ACHIEVEMENT = 3;
    //game action

    // finger vs bugs config
    public const int ANT_WORKER_LIFE_DEDUCTION = -1;
    public const int ANT_WARRIOR_LIFE_DEDUCTION = -2;
    public const int ANT_QUEEN_LIFE_DEDUCTION = -10;
    public const int SPIDER_LIFE_DEDUCTION = -1;
    public const int SMALL_SPIDER_LIFE_DEDUCTION = -1;
    public const int COCKROACH_LIFE_DEDUCTION = -5;

    public const int SCORE_PER_ANT_KILL = 1;

    // insect stats classic
    public const int CLASSIC_ANT_WORKER_HP = 1;
    public const float CLASSIC_ANT_WORKER_SPEED = 18.75f;

    public const int CLASSIC_ANT_WARRIOR_HP = 6;
    public const float CLASSIC_ANT_WARRIOR_SPEED = 5f;

    public const int CLASSIC_ANT_QUEEN_HP = 30;
    public const float CLASSIC_ANT_QUEEN_SPEED = 2f;

    public const int CLASSIC_SPIDER_HP = 1;
    public const float CLASSIC_SPIDER_SPEED = 26f;

    public const int CLASSIC_SMALL_SPIDER_HP = 1;
    public const float CLASSIC_SMALL_SPIDER_SPEED = 22f;

    public const int CLASSIC_COCKROACH_HP = 3;
    public const float CLASSIC_COCKROACH_SPEED = 38f;

    public const int KIDS_ANT_WORKER_HP = 1;
    public const float KIDS_ANT_WORKER_SPEED = 15f;

    public const int KIDS_ANT_WARRIOR_HP = 4;
    public const float KIDS_ANT_WARRIOR_SPEED = 5f;

    public const int KIDS_ANT_QUEEN_HP = 16;
    public const float KIDS_ANT_QUEEN_SPEED = 2f;

    public const int KIDS_SPIDER_HP = 1;
    public const float KIDS_SPIDER_SPEED = 18f;

    public const int KIDS_SMALL_SPIDER_HP = 1;
    public const float KIDS_SMALL_SPIDER_SPEED = 16f;

    public const int KIDS_COCKROACH_HP = 2;
    public const float KIDS_COCKROACH_SPEED = 25f;

    public const float REWARDED_BASED_VIDEO_KNOCK_BACK_VALUE = 40f;
    // finger vs bugs config

    //google play
    public const string appId = "563694885564";
    public const string KEYSTORE_PASS = "password99";
    public const string KEYSTORE_ALIAS_NAME = "gigadrillgames";
    public const string KEYSTORE_ALIAS_PASS = "password99";

    public const string leaderboard_classic_highscore = "CgkIvLXV9rMQEAIQCA";
    public const string leaderboard_kids_highscore = "CgkIvLXV9rMQEAIQCQ";

    public const string achievement_bug_hater = "CgkIvLXV9rMQEAIQCw";
    public const string achievement_bug_hunter = "CgkIvLXV9rMQEAIQDA";
    public const string achievement_ultimate_tapper = "CgkIvLXV9rMQEAIQEA";

    public const string achievement_casual_tapper = "CgkIvLXV9rMQEAIQDg";
    public const string achievement_pro_tapper = "CgkIvLXV9rMQEAIQDw";
    public const string achievement_bug_terminator = "CgkIvLXV9rMQEAIQDQ";

    public const string achievement_kids_bug_hater = "CgkIvLXV9rMQEAIQEQ";
    public const string achievement_kids_bug_hunter = "CgkIvLXV9rMQEAIQEg";
    public const string achievement_kids_bug_terminator = "CgkIvLXV9rMQEAIQEw";

    public const string achievement_kids_casual_tapper = "CgkIvLXV9rMQEAIQFA";
    public const string achievement_kids_pro_tapper = "CgkIvLXV9rMQEAIQFQ";
    public const string achievement_kids_ultimate_tapper = "CgkIvLXV9rMQEAIQFg";
    //google play

    //ios game center config
    public const string ITUNE_APP_ID = "1175782438";
    public const string ITUNE_APP_SKU = "gigadrillgames_fva_2016";
    public const string GAME_CENTER_LEADER_BOARD_CLASSIC = "fvb_classic_leaderboard";
    public const string GAME_CENTER_LEADER_BOARD_KIDS = "fvb_kids_leaderboard";

    public const string GAME_CENTER_KILL_100_ANTS = "fvb_squash_100_ants";
    public const string GAME_CENTER_KILL_300_ANTS = "fvb_squash_300_ants";
    public const string GAME_CENTER_KILL_500_ANTS = "fvb_squash_500_Ants";

    public const string GAME_CENTER_COMBO_50 = "fvb_combo_50";
    public const string GAME_CENTER_COMBO_100 = "fvb_combo_100";
    public const string GAME_CENTER_COMBO_500 = "fvb_combo_500";

    public const string GAME_CENTER_KIDS_KILL_100_ANTS = "fvb_kids_squash_100_bugs";
    public const string GAME_CENTER_KIDS_KILL_300_ANTS = "fvb_kids_squash_300_bugs";
    public const string GAME_CENTER_KIDS_KILL_500_ANTS = "fvb_kids_squash_500_bugs";

    public const string GAME_CENTER_KIDS_COMBO_50 = "fvb_kids_combo_50";
    public const string GAME_CENTER_KIDS_COMBO_100 = "fvb_kids_combo_100";
    public const string GAME_CENTER_KIDS_COMBO_500 = "fvb_kids_combo_500";


    //ios game center config
    // tutela earn without ads just by getting the info about the wifi signal or internet signal strength to make the
    // world internet connections faster
    public const string TUTELA_API_KEY = "blbbplbla0lrcki7uh98nbv599";
}

