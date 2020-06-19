
all: A/OutA.txt B/OutB.txt C/OutC.txt

A/OutA.txt: A/Makefile
	cd A && make

B/OutB.txt: B/Makefile
	cd B && make

C/OutC.txt: C/Makefile
	cd C && make

clean:
	$(RM) out* *.txt *.exe *.dll *.svg
	cd A && make clean
	cd B && make clean
	cd C && make clean
