all: plotA1.svg plotA2.svg plotA3.svg

plotA1.svg: plotA1.gpi out.plotA1.txt
	gnuplot $<

plotA2.svg: plotA2.gpi out.plotA2.txt
	gnuplot $<

plotA3.svg: plotA3.gpi out.plotA3.txt
	gnuplot $<

out.plotA1.txt out.plotA2.txt out.plotA3.txt: mainA.exe
	mono $< > out.plotA1.txt

mainA.exe: main.cs matlib.dll
	mcs $< -reference:$(lastword $^) -out:$@

LIBDIR = ../../../matlib
SRC :=$(LIBDIR)/matrix/vector.cs
SRC +=$(LIBDIR)/matrix/matrix.cs
SRC +=../ode.integrator.cs
SRC +=../../1-interpolation/C/cspline.cs

matlib.dll: $(SRC)
	mcs $^ -target:library -out:$@

clean:
	$(RM) out* *.exe *.dll *.svg
