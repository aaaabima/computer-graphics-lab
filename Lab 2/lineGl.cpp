#define GLUT_DISABLE_ATEXIT_HACK
#include <GL/glut.h>

#define KEY_ESC 27 /* glut doesn't define this */

GLvoid keyboard( GLubyte key, GLint x, GLint y)
{
  switch (key)
  {
  case KEY_ESC: /* exit when escape key is pressed */
    exit(0);
  break;
  }
}

GLvoid display()
{
  /* Do all your OpenGL rendering here */
  glClear(GL_COLOR_BUFFER_BIT);

  glColor3f(0.0,0.1,0.0);
  
  glPointSize(8.0);
  glLineWidth(5.0);

  glBegin(GL_LINE_STRIP);
    glVertex2f(0, 0);
    glVertex2f(1, 1);
    glVertex2f(-0.5, 0);
    glVertex2f(1, -1);
    glVertex2f(0, 0);
  glEnd();
  glFlush();
}

void init(void)
{
  /* set background color */
  glClearColor(1.0,1.0,0.0,1.0);
}

int main(int argc, char* argv[])
{
  GLint width;
  GLint height;

  glutInit(&argc, argv);
  glutInitDisplayMode(GLUT_SINGLE | GLUT_RGB);

  width = glutGet(GLUT_SCREEN_WIDTH);
  height = glutGet(GLUT_SCREEN_HEIGHT);
  glutInitWindowPosition(width/4, height/4);
  glutInitWindowSize(width/2, height/2);
  glutCreateWindow(argv[0]);

  glutDisplayFunc( display );
  glutKeyboardFunc( keyboard );

  init();

  glutMainLoop();

  return 0;
}
