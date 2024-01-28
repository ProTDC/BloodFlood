using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FunnyText : MonoBehaviour
{
    private TMP_Text text;

    private string[] funnies = 
    {
        "Now for IOS and Android!",
        "Better then sex (probably)",
        "When the blood is flood",
        "BloodFlood The Movie (coming soon)",
        "Nya :3",
        "The only game where the blood is truly flooding",
        "???",
        "What",
        "I will become back my money...",
        "Wimdovs",
        "Bwoodfwood",
        "Windows phone not supported",
        "Fun fact!",
        "Man im so hungry i could eat a...",
        "^_^",
        "Haiiiiiii!!111!!1",
        "BloodFlood: Modern Warfare Now in development",
        "Did i just do that?",
        "Crazy? I was crazy once",
        "Made with labor, blood, sweat and love",
        "XBOX LIVE!!",
        "Hope you bought the Deluxe edition",
        "The fishing mode is here!",
        "It's blood'n time",
        "Not as good as Nekopara: Catboys Paradise",
        "Ok but who floods the blood...",
        "2",
        "Finally!",
        "Made in Norway!",
        "It's a game!",
        "BloodFlood",
        "Yippi!",
        "Works with a keyboard",
        "Woah",
        "9/10 dentists recommend!",
        "GOTY edition",
        "Yes, sir!",
        "*",
        "Sic Mundus Creatus Est",
        "We did it Reddit!",
        "Very fun!",
        "!?",
        "Hampter",
        "Gaming!",
        "Contains features!",
        "Crying on enemies not included",
        "Sold separately",
        "Has a technology",
        "Get over here!",
        "Praise the sun!",
        "Impossible to analyze",
        "Aieou",
        "As seen on TV!",
        "Welcome!",
        "No poison swamps included",
        "It's going to happend again",
        "Thank you!",
        "Divine Link Established",
        ":pensive:",
        "<3",
        "So uh, yeah",
        "Kill the intruder!",
        "Features no vampire robots",
        "Don't deal with the devil",
        "Fortnite Battlepass",
        "Season 1 Battlepass out now!",
        "Hey there ^_^",
        "Linxus",
        "Hello World!",
        "Subscribe to Grifdar",
        "Subscribe to ProTDC",
        "This is not a Minecraft reference",
        "The new E-Sport",
        "Rubrub"


    };

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        string txt = funnies[Random.Range(0, funnies.Length)];
        text.text = txt;
    }
}
