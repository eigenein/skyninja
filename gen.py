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
    )

    for locale, translation in translations.items():
        print("[INFO] Locale:", locale)
        locale_path = pathlib.Path(".") / locale
        print("[INFO] Locale Path:", locale_path)
        for page_name, template_name in pages.items():
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
                slash_locale="/%s" % locale,
                **final_translation
            )
            print("[INFO] HTML:", len(page_body), "characters.")
            page_path.open("wt", encoding="utf-8").write(page_body)
            print("[ OK ]")


pages = {
    "": "index",
    "download": "download",
    "donate": "donate",
    "contact": "contact",
}

translations = {
    "": {
        "header_contact": "Contact",
        "header_donations": "Donations",
        "header_download": "Download",
        "header_home": "Home",
    },
    "ru": {
        "header_download": "Скачать",
    }
}

default_translation = translations[""]

if __name__ == "__main__":
    main()
