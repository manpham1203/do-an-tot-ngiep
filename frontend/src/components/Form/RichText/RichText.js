import { ContentState, convertToRaw, EditorState, Modifier } from "draft-js";
import draftToHtml from "draftjs-to-html";
import htmlToDraft from "html-to-draftjs";
import React, { useEffect, useState } from "react";
import { Editor } from "react-draft-wysiwyg";
import "react-draft-wysiwyg/dist/react-draft-wysiwyg.css";

function RichText({ onChange, value, ...props }) {
  const [editorState, setEditorState] = useState(EditorState.createEmpty());
  const [updated, setUpdated] = useState(false);
  useEffect(() => {
    if (!updated) {
      const defaultValue = value ? value : "";
      const blocksFromHtml = htmlToDraft(defaultValue);
      const contentState = ContentState.createFromBlockArray(
        blocksFromHtml.contentBlocks,
        blocksFromHtml.entityMap
      );
      const newEditorState = EditorState.createWithContent(contentState);
      setEditorState(newEditorState);
    }
  }, [value]);
  const onEditorStateChange = (editorState) => {
    setUpdated(true);
    setEditorState(editorState);

    return onChange(draftToHtml(convertToRaw(editorState.getCurrentContent())));
  };
  return (
    <React.Fragment>
      <div className="editor">
      <label
        className="block mb-2 text-sm font-medium text-gray-900 dark:text-gray-300"
      >
       {props.label}
      </label>
        <Editor
          toolbarClassName="toolbarClassName"
          wrapperClassName="bg-white border border-input-border"
          editorClassName="border border-input-border p-[20px] h-[200px]"
          editorState={editorState}
          onEditorStateChange={onEditorStateChange}
        />
      </div>
    </React.Fragment>
  );
}

export default RichText;
