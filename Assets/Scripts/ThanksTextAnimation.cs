using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Util;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    internal class ThanksTextAnimation : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        void Awake()
        {
            _text.text = Texts.PickRandom();
        }

        private static readonly string[] Texts =
        {
            "Thanks!",
            "Gracias!",
            "Danke!",
            "Merci!",
            "Thank you!",
            "TY!",
            "Thx!",
            "Cheers!",
            "Appreciate!",
            "Many tnx!",
            "Much thx!",
            "Ta!",
            "Big thx!",
            "Grazie!",
            "Grateful!",
            "Obrigado!",
            "Gratitudes!",
            "Gr8 job!",
            "Superb!",
            "Arigato!",
            "Tak!",
            "Bless you!",
            "High five!",
            "U rock!",
            "Thumbs up!",
            "Thanks a ton!",
            "Glad 4 U!",
            "Ur awesome!",
            "Kudos!",
            "Hat's off!",
            "Thanks pal!",
            "Xie xie!",
            "Spasibo!",
            "Takk!",
            "Tesekkurler!",
            "Dhanyavad!",
            "Salute!",
            "Ur amazing!",
            "Tnx a bunch!",
            "Top-notch!",
            "Tnx a mill!",
            "Bedankt!",
            "Domo!",
            "Asante!",
            "Much oblige!",
            "Hvala!",
            "Mahalo!",
            "De nada!",
            "Shukran!",
            "You rule!",
            "Gr8 delivry!",
            "Deliv'd! Thx!",
            "Fast deliv!",
            "On time! TY!",
            "Safe deliv!",
            "Got it, tnx!",
            "Package TY!",
            "Thx 4 deliv!",
            "Quick del!",
            "Box rec'd!",
            "Parcel tnx!",
            "In my hands!",
            "Thx, got it!",
            "Item rcvd!",
            "Handled w/c!",
            "Thx 4 quick!",
            "Efficient!",
            "Thx, driver!",
            "Precise!",
            "Punctual!",
            "Unbox tnx!",
            "Just in time!",
            "Thx 4 drop!",
            "Speedy del!",
            "Reliable!",

        };
    }
}
