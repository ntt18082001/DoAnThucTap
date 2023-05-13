import * as React from 'react';
import '../../../assets/css/error.css';
import { NavLink } from 'react-router-dom';

export interface NotFoundProps {}

export default function NotFound(props: NotFoundProps) {
  return (
    <>
      <div className="container-nf" style={{ height: '89vh' }}>
        <div className="left-nf" style={{ margin: 'auto 0' }}>
          <img alt="" src={require('../../../assets/img/dino.png')} />
        </div>
        <div className="right-nf" style={{ margin: 'auto 0' }}>
          <h2>404 Error!</h2>
          <p>Không tìm thấy trang này!</p>
          <div className="link-nf">
            <NavLink to="/">Trang chủ</NavLink>
          </div>
        </div>
      </div>
    </>
  );
}
