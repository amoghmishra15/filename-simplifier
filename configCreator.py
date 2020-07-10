defaultConfig ='''
{
    "extensions": [
        {
            "type": "Video",
            "value": ".mp4 .m4v .mkv .mov .avi .flv .ass .srt .wmv .webm"
        },
        {
            "type": "Audio",
            "value": ".mp3 .flac .wav .ogg"
        },
        {
            "type": "Document",
            "value": ".txt .pdf"
        },
        {
            "type": "Custom",
            "value": ".custom1 .custom2"
        },
        {
            "type": "Everything (including folders)",
            "value": ""
        }
    ],
    "defaultBlacklist": "weekly 4k v1x v2x complete hevc x264 h264 x265 h265 batch bluray webrip bdrip dvdrip cdrip hdrip",
    "customBlacklist": "customBL1 customBL2 customBL3",
    "flags": [
        {
            "selectedExtension": "0",
            "rmSquareBracket": true,
            "rmParentheses": true,
            "rmDash": true,
            "rmForeign": true,
            "enDefaultBlacklist": true,
            "enCustomBlacklist": false,
            "enTitleCase": true
        }
    ]
}
'''

def makeConfig():
    with open("config.json", "w") as f:
        f.write(defaultConfig)
