import React from 'react';
import 'lightbox.js-react/dist/index.css';
import { SlideshowLightbox } from 'lightbox.js-react';
import { baseURL } from 'endpoints';

interface ImageItemProps {
  alt: string;
  src: string;
}

function ImageItem(props: ImageItemProps) {
  const { src, alt } = props;

  return (
    <SlideshowLightbox className="container grid grid-cols-3 gap-2 mx-auto">
      <img className="w-full rounded image-screen" alt={alt} src={`${baseURL}/${src}`} loading="lazy" />
    </SlideshowLightbox>
  );
}
export default React.memo(ImageItem);
