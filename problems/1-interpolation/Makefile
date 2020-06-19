
all: A/PlotA.svg B/PlotB.svg C/PlotC.svg

A/PlotA.svg: A/Makefile
	cd A && make

B/PlotB.svg: B/Makefile
	cd B && make

C/PlotC.svg: C/Makefile
	cd C && make

clean:
	$(RM) out* *.txt *.exe *.dll *.svg
	cd A && make clean
	cd B && make clean
	cd C && make clean
