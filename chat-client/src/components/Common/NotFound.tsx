import * as React from 'react';
import { Outlet } from 'react-router-dom';

export interface NotFoundProps {
}

export default function NotFound (props: NotFoundProps) {
  return (
    <>
       <Outlet />
      NotFound
    </>
  );
}
