#define GLUT_DISABLE_ATEXIT_HACK
#include <GL/glut.h> // (or others, depending on the system in use)

void init (void)
{
	glClearColor (1.0, 1.0, 1.0, 0.0); // Set display-window color to white.
	glMatrixMode (GL_PROJECTION); // Set projection parameters.
	gluOrtho2D (0.0, 200.0, 0.0, 150.0);
}

void dot (void)
{
	glClear (GL_COLOR_BUFFER_BIT); // Clear display window.
	glColor3f (0.0, 0.0, 1.0); // Set line segment color to red.
	// SIZE
	glPointSize(12.0f);
	glBegin (GL_POINTS);
	// COLOR
	glColor3f (0.0, 0.0, 0.0); ///black
	glVertex2f (100, 75);
	glColor3f (1.0, 0.0, 0.0); //red
	glVertex2f (50, 35);
	glColor3f (0.0, 0.0, 1.0); //blue
	glVertex2f (50, 110);
	glColor3f (0.0, 1.0, 0.0); //green
	glVertex2f (150, 35);
	glColor3f(0.0, 1.0, 1.0); //cyan
	glVertex2f (150, 110);
	glColor3f(0.5, 0.5, 0.5); //Warna baru
	glVertex2f (175, 110);
	glEnd ();
	glFlush ( ); // Process all OpenGL routines as quickly as possible.
}

int main (int argc, char** argv)
{
	glutInit (&argc, argv); // Initialize GLUT.
	glutInitDisplayMode (GLUT_SINGLE | GLUT_RGB); // Set display mode.
	glutCreateWindow ("An Example OpenGL Program"); // Create display window.
	init ( ); // Execute initialization procedure.
	glutDisplayFunc (dot); // Send graphics to display window.
	glutMainLoop ( ); // Display everything and wait.
}
