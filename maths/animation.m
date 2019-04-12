mint = 0;
maxt = 1;
nb = 100;

figure;

[mercuryOrbitX,  mercuryOrbitY,   mercuryOrbitZ] =  orbit(0:0.241/1000:0.241, 0.3871, 252.25, 0.20564, 7.006, 77.46, 48.34, 0.241);
[venusOrbitX,    venusOrbitY,     venusOrbitZ] =    orbit(0:0.615/1000:0.615, 0.7233, 181.98, 0.00676, 3.398, 131.77, 76.67, 0.615);
[earthOrbitX,    earthOrbitY,     earthOrbitZ] =    orbit(0:1/1000:1, 1, 100.47, 0.01673, 0, 102.93, 0, 1);
[marsOrbitX,     marsOrbitY,      marsOrbitZ] =     orbit(0:1.881/1000:1.881, 1.5237, 355.43, 0.09337, 1.852, 336.08, 49.71, 1.881);
[jupiterOrbitX,  jupiterOrbitY,   jupiterOrbitZ] =  orbit(0:11.87/1000:11.87, 5.2025, 34.33, 0.04854, 1.299, 14.27, 100.29, 11.87);

[cometOrbitX,  cometOrbitY,   cometOrbitZ] =  orbit(0:0.5/1000:0.5, 7, 0, 0.95, 45, 0, 0, 0.5);

for t = mint:(maxt-mint)/nb:maxt

    clf();
    
    plot3(mercuryOrbitX, mercuryOrbitY, mercuryOrbitZ, '-b');
    hold on;
    plot3(venusOrbitX, venusOrbitY, venusOrbitZ, '-r');
    plot3(earthOrbitX, earthOrbitY, earthOrbitZ, '-g');
    plot3(marsOrbitX, marsOrbitY, marsOrbitZ, '-y');
    plot3(jupiterOrbitX, jupiterOrbitY, jupiterOrbitZ, '-m');
    plot3(cometOrbitX, cometOrbitY, cometOrbitZ, '-k');

    [mercuryX, mercuryY, mercuryZ] = orbit(t, 0.3871, 252.25, 0.20564, 7.006, 77.46, 48.34, 0.241);
    plot3(mercuryX, mercuryY, mercuryZ, 'ob');

    [venusX, venusY, venusZ] = orbit(t, 0.7233, 181.98, 0.00676, 3.398, 131.77, 76.67, 0.615);
    plot3(venusX, venusY, venusZ, 'or');

    [earthX, earthY, earthZ] = orbit(t, 1, 100.47, 0.01673, 0, 102.93, 0, 1);
    plot3(earthX, earthY, earthZ, 'og');

    [marsX, marsY, marsZ] = orbit(t, 1.5237, 355.43, 0.09337, 1.852, 336.08, 49.71, 1.881);
    plot3(marsX, marsY, marsZ, 'oy');

    [jupiterX, jupiterY, jupiterZ] = orbit(t, 5.2025, 34.33, 0.04854, 1.299, 14.27, 100.29, 11.87);
    plot3(jupiterX, jupiterY, jupiterZ, 'om');

    [cometX, cometY, cometZ] = orbit(t, 7, 0, 0.95, 45, 0, 0, 0.5);
    plot3(cometX, cometY, cometZ, 'ok');

    axis([-10 10 -10 10 -10 10])
    legend("Mercury", "Venus", "Earth", "Mars", "Jupiter");

    drawnow

end