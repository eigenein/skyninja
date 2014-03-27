#!/usr/bin/env python3
# coding: utf-8

import sys
assert sys.hexversion >= 0x03040000, "Python 3.4 is required"

import pathlib

import pystache


def main():
    renderer = pystache.renderer.Renderer(
        file_encoding="utf-8",
        file_extension="mustache",
        search_dirs=["templates"],
        string_encoding="utf-8",
        missing_tags="strict",
    )

    for locale, translation in translations.items():
        print("[INFO] Locale:", locale)
        locale_path = pathlib.Path(".") / locale
        print("[INFO] Locale Path:", locale_path)
        for page_name, (template_name, title_key) in pages.items():
            print("[INFO] Page:", page_name or "<empty>")
            print("[INFO] Template:", template_name)
            page_path = locale_path / page_name / "index.html"
            print("[INFO] Page Path:", page_path)
            page_directory = page_path.parent
            assert not page_directory.is_file(), "%s is a file" % page_directory
            if not page_directory.exists():
                page_directory.mkdir(parents=True)
                print("[ OK ] Directory is created.")
            else:
                print("[INFO] Directory exists:", page_directory)
            print("[INFO] Rendering ...")
            final_translation = dict(default_translation)
            final_translation.update(translation)
            page_body = renderer.render_name(
                template_name,
                page_name=page_name,
                title=final_translation.get(title_key),
                locale=locale or "en",
                slash_locale=("/%s" % locale) if locale else "",
                **final_translation
            )
            print("[INFO] HTML:", len(page_body), "characters.")
            page_path.open("wt", encoding="utf-8").write(page_body)
            print("[ OK ]")


pages = {
    "": ("index", None),
    "download": ("download", "header_download"),
    "donate": ("donate", "header_donations"),
    "contact": ("contact", "header_contact"),
}

translations = {
    "": {
        "eigenein_fullname": "Pavel Perestoronin",
        "footer_facebook": "Facebook",
        "footer_twitter": "Twitter",
        "footer_vk": "VKontakte",
        "header_contact": "Contact",
        "header_donations": "Donations",
        "header_download": "Download",
        "header_home": "Home",
        "header_skyninja": "SkyNinja",
    },
    "ru": {
        "eigenein_fullname": "eigenein",
        "footer_facebook": "Фейсбук",
        "footer_twitter": "Твиттер",
        "footer_vk": "ВКонтакте",
        "header_download": "Скачать",
    }
}

default_translation = translations[""]

if __name__ == "__main__":
    main()
