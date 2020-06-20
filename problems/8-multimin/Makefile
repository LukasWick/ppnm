
all: A/OutA B/OutB ReadMe.md
	cat ReadMe.md

A/OutA: A/Makefile
	cd A && make

B/OutB: B/Makefile
	cd B && make
	
C/OutC: C/Makefile
	cd C && make


clean:
	$(RM) out* *.txt *.exe *.dll *.svg
	cd A && make clean
	cd B && make clean
	cd C && make clean
