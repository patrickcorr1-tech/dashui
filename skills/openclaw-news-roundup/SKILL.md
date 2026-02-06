---
name: openclaw-news-roundup
description: Daily or ad-hoc roundup of OpenClaw news and new skills. Use when asked to compile updates from ClawHub, OpenClaw docs/blog, GitHub, Discord announcements, or X/Twitter and summarize into a short briefing with links.
---

# OpenClaw News Roundup

## Overview
Produce a concise briefing of OpenClaw news and new skills with links, suitable for a daily Telegram update or on-demand request.

## Workflow

### 1) Confirm scope and delivery
- If not specified, ask for: timezone, delivery target (Telegram or chat), and sources.
- Default sources (if user requests “OpenClaw news + skills”):
  - ClawHub (new skills)
  - OpenClaw docs/blog
  - OpenClaw GitHub repo (releases/commits)
  - Discord announcements
  - X/Twitter posts

### 2) Collect sources
Use `web_search` to find the most recent updates, then `web_fetch` for the top items to confirm details.
Suggested queries:
- "site:clawhub.com OpenClaw skills" or "ClawHub OpenClaw skill"
- "OpenClaw blog" / "OpenClaw docs updates"
- "OpenClaw GitHub releases" / "openclaw github"
- "OpenClaw Discord announcements"
- "OpenClaw X Twitter"

### 3) Select items
- Pick 5–10 items total.
- Prefer official sources, release notes, and newly published skills.
- If the day is quiet, include fewer items and say “No major updates today.”

### 4) Format the briefing
Use short bullets, each with:
- Title + source
- One‑line summary
- Link

Example:
- **New Skill: X** (ClawHub) — One‑line description. <link>

### 5) Deliver
- If the user requested Telegram, send via `message` tool to their Telegram user ID.
- Otherwise reply in the current chat.
