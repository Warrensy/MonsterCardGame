Für viele grundlegende Funktionen von MonsterTradingCardGame wird eine Datenbank anbindung benötigt.
Für die Daten verwaltung wurde die Software PostrgeSQL verwendet. 
Um eine kopie der original Datenbank zu erstellen folgen Sie der Anleitung bis inkl. Schritt 4.
Bei Schritt 5 werden Datensätze eingefügt, sollten diese auch erwünscht sein, führen Sie diesen Schritt ebenfalls aus.

<<Anleitung>>

1.Öffnen sie die postgresql Anwendung.
2.Erstellen sie mit folgendem Befehl eine neue Datenbank:

CREATE DATABASE monstercardgame;

3.Verbinden sie sich mit dem Befehl "\c monstercardgame" mit der eben erstellten Datenbank.
4.Erstellen sie alle notwendigen Tabellen, Referencen und Einstellungen der original Datenbank mit folgenden Befehlen.

create table cards
(
    cardid    serial
        primary key,
    name      varchar(25) not null,
    element   integer     not null,
    element_w integer     not null,
    race      integer     not null,
    race_w    integer     not null,
    dmg       integer     not null
);

alter table cards
    owner to postgres;

create table users
(
    userid      integer             not null
        primary key,
    password    varchar(50)         not null,
    elo         integer default 100 not null,
    patreon     integer,
    username    varchar(30)         not null,
    coins       integer default 20  not null,
    gameswon    integer default 0   not null,
    gamesplayed integer default 0   not null,
    gild        integer default 0   not null
);

alter table users
    owner to postgres;

create table stack
(
    id        serial
        primary key,
    cardid    integer not null
        references cards
            on delete cascade,
    userid    integer not null
        references users
            on delete cascade,
    status    boolean default false,
    intrading boolean default false
);

alter table stack
    owner to postgres;

create table trade
(
    tradeid    serial,
    cardid     integer
        references cards
            on delete cascade,
    userid     integer
        references users
            on delete cascade,
    wantedtype integer,
    wanteddmg  integer
);

alter table trade
    owner to postgres;
	
alter table trade
    owner to postgres;

5.Einfügen von Nutzerdaten mit folgenden Befehlen:

INSERT INTO cards (name,element,element_w,race,race_w,dmg) VALUES ('Fire Dragon','1','0','3','7','7');
INSERT INTO cards (name,element,element_w,race,race_w,dmg) VALUES ('Shining Knight','2','1','5','8','2');
INSERT INTO cards (name,element,element_w,race,race_w,dmg) VALUES ('Goblin','2','1','0','3','4');
INSERT INTO cards (name,element,element_w,race,race_w,dmg) VALUES ('Kraken','0','2','6','10','7');
INSERT INTO cards (name,element,element_w,race,race_w,dmg) VALUES ('Fire Elves','1','0','7','10','4');
INSERT INTO cards (name,element,element_w,race,race_w,dmg) VALUES ('Ork','2','1','4','1','4');
INSERT INTO cards (name,element,element_w,race,race_w,dmg) VALUES ('Water Spell','0','2','2','6','8');
INSERT INTO cards (name,element,element_w,race,race_w,dmg) VALUES ('Fire Spell','1','0','2','6','3');
INSERT INTO cards (name,element,element_w,race,race_w,dmg) VALUES ('Normal Spell','2','1','2','6','3');
INSERT INTO cards (name,element,element_w,race,race_w,dmg) VALUES ('Fire Wizzard','1','0','1','10','8');
INSERT INTO cards (name,element,element_w,race,race_w,dmg) VALUES ('Tsunami','0','2','2','6','6');
INSERT INTO cards (name,element,element_w,race,race_w,dmg) VALUES ('Firestorm','1','0','2','6','7');
INSERT INTO cards (name,element,element_w,race,race_w,dmg) VALUES ('Sky Bolder','2','1','2','6','7');
INSERT INTO cards (name,element,element_w,race,race_w,dmg) VALUES ('Waterbording','0','2','2','6','4');
INSERT INTO cards (name,element,element_w,race,race_w,dmg) VALUES ('Single Drop of Water','0','2','2','6','1');
INSERT INTO cards (name,element,element_w,race,race_w,dmg) VALUES ('Lord Groblin','2','1','0','3','9');
INSERT INTO cards (name,element,element_w,race,race_w,dmg) VALUES ('Born Chiller','2','1','1','10','1');
INSERT INTO cards (name,element,element_w,race,race_w,dmg) VALUES ('Blaze Atronarch','1','0','11','2','7');
INSERT INTO cards (name,element,element_w,race,race_w,dmg) VALUES ('Haunted Atronarch','2','1','11','2','8');
INSERT INTO cards (name,element,element_w,race,race_w,dmg) VALUES ('Drowned Atronarch','0','2','11','2','7');
INSERT INTO cards (name,element,element_w,race,race_w,dmg) VALUES ('Attack Helicopter','2','1','11','2','11');

Stelle sicher, dass der erste Eintrag in cards die cardid = 1 hat. Ansonten kann es zu Problemen kommen.
Sollte der erste Eintrag nicht mit der ID "1" anfangen, sollte der Befehl  "ALTER SEQUENCE product_id_seq RESTART WITH 1"
