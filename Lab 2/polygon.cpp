#define GLUT_DISABLE_ATEXIT_HACK
#include <GL/glut.h>

void mydisplay()
{
  glClear(GL_COLOR_BUFFER_BIT);
  glBegin(GL_POLYGON);
    glVertex2f(-1.0, 0.0);
    glVertex2f(-0.8, -0.8);
    glVertex2f(0.8, -0.8);
    glVertex2f(0.8, 0.8); 
    glVertex2f(0.4, 1.0);
  glEnd();
  glFlush();
}

int main(int argc, char** argv) 
{
  glutInit(&argc, argv);
  glutCreateWindow("simple");
  glutDisplayFunc(mydisplay);
  glutMainLoop();
}
