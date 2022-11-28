CREATE TABLE access_points
(
    access_point_id INTEGER PRIMARY KEY AUTOINCREMENT,
    ssid            TEXT NOT NULL,
    bssid           TEXT NOT NULL
);

CREATE TABLE locations
(
    location_id INTEGER PRIMARY KEY AUTOINCREMENT,
    pos_x       REAL    NOT NULL,
    pos_y       REAL    NOT NULL,
    floor       INTEGER NOT NULL
);

CREATE TABLE ap_locations
(
    access_point_id INTEGER REFERENCES access_points (access_point_id),
    location_id     INTEGER REFERENCES locations (location_id),
    signal_strength INTEGER NOT NULL,
    PRIMARY KEY (access_point_id, location_id)
);

----------------------------------------------------------

CREATE TABLE location_paths
(
    point_a INTEGER REFERENCES locations (location_id),
    point_b INTEGER REFERENCES locations (location_id),
    PRIMARY KEY (point_a, point_b),
    CHECK (point_a <> point_b)
);
