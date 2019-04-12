function [x, y, z] = orbit(t, a, ml0, e, I, omega, Omega, T)
    
    I = I*pi/180;
    omega = omega/180*pi;
    Omega = Omega/180*pi;

    n = 2*pi/T;
    theta = (ml0 + omega) / n;
    M = n*(t-theta);
    E = 0;
    maxIter = 30;
    for i = 1:maxIter
        E = M + e * sin(E);
    end

    Theta = 2*atan(sqrt((1+e)/(1-e)).*tan(E/2));
    r = a * (1 - e * cos(E));

    r = a * (1 - e * cos(E));
    x = r .* (cos(Omega) .* cos(omega+Theta) - sin(Omega) .* sin(omega+Theta) .* cos(I));
    y = r .* (sin(Omega) .* cos(omega+Theta) + cos(Omega) .* sin(omega+Theta) .* cos(I));
    z = r .* sin(omega+Theta) .* sin(I);
end